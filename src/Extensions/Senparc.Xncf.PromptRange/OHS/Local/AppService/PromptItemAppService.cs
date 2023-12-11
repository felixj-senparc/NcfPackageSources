﻿using Microsoft.AspNetCore.Mvc;
using Senparc.CO2NET;
using Senparc.CO2NET.WebApi;
using Senparc.Ncf.Core.AppServices;
using Senparc.Ncf.Repository;
using Senparc.Xncf.PromptRange.Domain.Services;
using Senparc.Xncf.PromptRange.OHS.Local.PL.Request;
using Senparc.Xncf.PromptRange.OHS.Local.PL.response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Senparc.Ncf.Core.Exceptions;
using Senparc.Xncf.PromptRange.OHS.Local.PL.Response;

namespace Senparc.Xncf.PromptRange.OHS.Local.AppService
{
  public class PromptItemAppService : AppServiceBase
  {
    private readonly RepositoryBase<PromptItem> _promptItemRepository;
    private readonly PromptItemService _promptItemService;
    private readonly LlmModelService _llmModelService;
    private readonly PromptResultService _promptResultService;

    public PromptItemAppService(IServiceProvider serviceProvider,
      RepositoryBase<PromptItem> promptItemRepository,
      PromptItemService promptItemService,
      LlmModelService llmModelService,
      PromptResultService promptResultService) : base(serviceProvider)
    {
      _promptItemRepository = promptItemRepository;
      _promptItemService = promptItemService;
      _llmModelService = llmModelService;
      _promptResultService = promptResultService;
    }

    [ApiBind(ApiRequestMethod = ApiRequestMethod.Post)]
    public async Task<AppResponseBase<PromptItem_AddResponse>> Add(PromptItem_AddRequest request)
    {
      return await this.GetResponseAsync<AppResponseBase<PromptItem_AddResponse>, PromptItem_AddResponse>(
        async (response, logger) =>
        {
          // save promptItem
          var promptItem = await _promptItemService.AddPromptItemAsync(request);
          if (promptItem == null)
          {
            throw new NcfExceptionBase("添加失败");
          }

          var promptItemResponseDto = new PromptItem_AddResponse(promptItem.Content, DateTime.Now, promptItem.Version,
            promptItem.ModelId, promptItem.MaxToken, promptItem.Temperature, promptItem.TopP,
            promptItem.FrequencyPenalty, promptItem.StopSequences);

          // 是否立即生成结果，暂时不添加这个开关
          if (true)
          {
            // 如果立即生成，就根据numsOfResults立即生成
            for (var i = 0; i < request.NumsOfResults; i++)
            {
              // 分别生成结果
              // var promptResult = await _promptResultService.GenerateResultAsync(promptItem);
              var promptResult = await _promptResultService.SenparcGenerateResultAsync(promptItem);
              promptItemResponseDto.PromptResultList.Add(promptResult);
            }
          }

          return promptItemResponseDto;
          // return respDto;
        });
    }


    [ApiBind]
    public async Task<AppResponseBase<List<PromptItem_GetIdAndNameResponse>>> GetIdAndName()
    {
      return await
        this.GetResponseAsync<AppResponseBase<List<PromptItem_GetIdAndNameResponse>>,
          List<PromptItem_GetIdAndNameResponse>>(async (response, logger) =>
        {
          return (await _promptItemService.GetFullListAsync(p => true, p => p.Id,
              Ncf.Core.Enums.OrderingType.Ascending))
            .Select(p => new PromptItem_GetIdAndNameResponse
            {
              Id = p.Id,
              Name = p.Name
            }).ToList();
        });
    }

    [ApiBind]
    public async Task<AppResponseBase<PromptItem_GetResponse>> Get(int id)
    {
      return await this.GetResponseAsync<AppResponseBase<PromptItem_GetResponse>, PromptItem_GetResponse>(
        async (response, logger) =>
        {
          var prompt = await _promptItemService.GetObjectAsync(p => p.Id == id);
          var model = await _llmModelService.GetObjectAsync(p => p.Id == prompt.ModelId);
          var result = await _promptResultService.GetObjectAsync(p => p.PromptItemId == prompt.Id);

          return new PromptItem_GetResponse()
          {
            PromptName = prompt.Name,
            Show = prompt.Show,
            Version = prompt.Version,
            ModelName = model.GetModelId(),
            ResultString = result.ResultString,
            Score = Convert.ToInt32(result.RobotScore > 0 ? result.RobotScore : result.HumanScore),
            Time = prompt.AddTime.ToString("yyyy-MM-dd")
          };
        });
    }


    [ApiBind]
    public async Task<AppResponseBase<List<PromptItem_GetResponse>>> GetVersionInfoList()
    {
      return await this
        .GetResponseAsync<AppResponseBase<List<PromptItem_GetResponse>>, List<PromptItem_GetResponse>>(
          async (response, logger) =>
          {
            var promptItemList = await _promptItemService.GetFullListAsync(p => true, p => p.Id,
              Ncf.Core.Enums.OrderingType.Ascending);

            List<int> modelIdList = promptItemList.Select(p => p.ModelId).Distinct().ToList();

            var modelList = await _llmModelService.GetFullListAsync(p => modelIdList.Contains(p.Id),
              p => p.Id, Ncf.Core.Enums.OrderingType.Ascending);

            return promptItemList.Select(item => new PromptItem_GetResponse()
              {
                Version = item.Version,
                Time = item.AddTime.ToString("yyyy-MM-dd"),
                ModelName = modelList.FirstOrDefault(p => p.Id == item.ModelId)?.GetModelId(),
                PromptName = item.Name,
              })
              .ToList();
          });
    }

    [ApiBind(ApiRequestMethod = ApiRequestMethod.Post)]
    public async Task<StringAppResponse> Modify(PromptItem_ModifyRequest req)
    {
      return await this.GetResponseAsync<StringAppResponse, string>(async (response, logger) =>
      {
        var result = await _promptItemService.GetObjectAsync(p => p.Id == req.Id) ??
                     throw new Exception("未找到prompt");

        result.ModifyName(req.Name);

        await _promptItemService.SaveObjectAsync(result);

        return "ok";
      });
    }

    [ApiBind(ApiRequestMethod = ApiRequestMethod.Delete)]
    public async Task<StringAppResponse> Del([FromQuery] int id)
    {
      return await this.GetResponseAsync<StringAppResponse, string>(async (response, logger) =>
      {
        var result = await _promptItemService.GetObjectAsync(p => p.Id == id) ??
                     throw new Exception("未找到prompt");

        await _promptItemService.DeleteObjectAsync(result);

        return "ok";
      });
    }
  }
}