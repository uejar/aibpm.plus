﻿using System.Threading.Tasks;
using ZhonTai.Admin.Domain.DictionaryType;
using ZhonTai.Admin.Domain.Dictionary;
using ZhonTai.Admin.Domain.Api;
using ZhonTai.Admin.Domain.Permission;
using ZhonTai.Admin.Domain.User;
using ZhonTai.Admin.Domain.Role;
using ZhonTai.Admin.Domain.UserRole;
using ZhonTai.Admin.Domain.RolePermission;
using ZhonTai.Admin.Domain.Tenant;
using ZhonTai.Admin.Domain.TenantPermission;
using ZhonTai.Admin.Domain.PermissionApi;
using ZhonTai.Admin.Domain.View;
using ZhonTai.Admin.Core.Configs;
using ZhonTai.Admin.Domain.Org;
using ZhonTai.Admin.Domain.UserStaff;
using ZhonTai.Admin.Core.Db.Data;
using ZhonTai.Admin.Domain.UserOrg;
using System.Linq;
using ZhonTai.Common.Extensions;
using AI.BPM.Domain.WorkflowTemplate;

namespace ZhonTai.Admin.Repositories;

public class CustomSyncData : SyncData, ISyncData
{
    public virtual async Task SyncDataAsync(IFreeSql db, DbConfig dbConfig = null, AppConfig appConfig = null)
    {
        using var uow = db.CreateUnitOfWork();
        using var tran = uow.GetOrBeginTransaction();
        var isTenant = appConfig.Tenant;

        var dictionaryTypes = GetData<WorkflowTemplateEntity>(isTenant, "InitData/BPM");
        await InitDataAsync(db, uow, tran, dictionaryTypes, dbConfig);
 
       /*  
        var permissionTree = GetData<PermissionEntity>(path: dbConfig.SyncDataPath);
        await InitDataAsync(db, uow, tran, permissionTree.ToList().ToPlainList((a) => a.Childs).ToArray(), dbConfig);
       */  
        uow.Commit();
    }
}
