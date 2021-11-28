﻿using Senparc.Ncf.Core.Enums;
using Senparc.Ncf.XncfBase;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Senparc.Ncf.Core.Config;

namespace Senparc.Xncf.XncfModuleManager
{
    [XncfRegister]
    public partial class Register : XncfRegisterBase, IXncfRegister
    {
        #region IXncfRegister 接口

        public override string Name => "Senparc.Xncf.XncfModuleManager";

        public override string Uid => SiteConfig.SYSTEM_XNCF_MODULE_XNCF_MODULE_MANAGER_UID;// "00000000-0000-0000-0000-000000000004";

        public override string Version => "0.1";//必须填写版本号

        public override string MenuName => "XNCF 模块管理";

        public override string Icon => "fa fa-user-secret";//fa fa-cog

        public override string Description => "XNCF 模块管理";

        public override async Task InstallOrUpdateAsync(IServiceProvider serviceProvider, InstallOrUpdate installOrUpdate)
        {
        }

        public override async Task UninstallAsync(IServiceProvider serviceProvider, Func<Task> unsinstallFunc)
        {
            await unsinstallFunc().ConfigureAwait(false);
        }
        #endregion

        public override IServiceCollection AddXncfModule(IServiceCollection services, IConfiguration configuration)
        {
            return base.AddXncfModule(services, configuration);
        }
    }
}