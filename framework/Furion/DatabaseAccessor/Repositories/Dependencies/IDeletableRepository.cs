﻿// -----------------------------------------------------------------------------
// 让 .NET 开发更简单，更通用，更流行。
// Copyright © 2020-2021 Furion, 百小僧, Baiqian Co.,Ltd.
//
// 框架名称：Furion
// 框架作者：百小僧
// 框架版本：2.8.3
// 源码地址：Gitee： https://gitee.com/dotnetchina/Furion
//          Github：https://github.com/monksoul/Furion
// 开源协议：Apache-2.0（https://gitee.com/dotnetchina/Furion/blob/master/LICENSE）
// -----------------------------------------------------------------------------

using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 可删除仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public partial interface IDeletableRepository<TEntity> : IDeletableRepository<TEntity, MasterDbContextLocator>
        where TEntity : class, IPrivateEntity, new()
    {
    }

    /// <summary>
    /// 可删除仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
    public partial interface IDeletableRepository<TEntity, TDbContextLocator> : IPrivateDeletableRepository<TEntity>
        where TEntity : class, IPrivateEntity, new()
        where TDbContextLocator : class, IDbContextLocator
    {
    }

    /// <summary>
    /// 可删除仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IPrivateDeletableRepository<TEntity> : IPrivateRootRepository
        where TEntity : class, IPrivateEntity, new()
    {
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> Delete(TEntity entity);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(params TEntity[] entities);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        void Delete(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> DeleteAsync(TEntity entity);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task DeleteAsync(params TEntity[] entities);

        /// <summary>
        /// 删除多条记录
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task DeleteAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>代理中的实体</returns>
        EntityEntry<TEntity> DeleteNow(TEntity entity);

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns></returns>
        EntityEntry<TEntity> DeleteNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        void DeleteNow(params TEntity[] entities);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        void DeleteNow(TEntity[] entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        void DeleteNow(IEnumerable<TEntity> entities);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        void DeleteNow(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除一条记录并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>代理中的实体</returns>
        Task<EntityEntry<TEntity>> DeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(params TEntity[] entities);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(TEntity[] entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(TEntity[] entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

        /// <summary>
        /// 删除多条记录并立即提交
        /// </summary>
        /// <param name="entities">多个实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">取消异步令牌</param>
        /// <returns>Task</returns>
        Task DeleteNowAsync(IEnumerable<TEntity> entities, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        void Delete(object key);

        /// <summary>
        /// 根据主键删除一条记录
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns>Task</returns>
        Task DeleteAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        void DeleteNow(object key);

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        void DeleteNow(object key, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        Task DeleteNowAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 根据主键删除一条记录并立即提交
        /// </summary>
        /// <param name="key">主键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        Task DeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        EntityEntry<TEntity> FakeDelete(TEntity entity);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteAsync(TEntity entity);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns></returns>
        EntityEntry<TEntity> FakeDeleteNow(TEntity entity);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns></returns>
        EntityEntry<TEntity> FakeDeleteNow(TEntity entity, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteNowAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteNowAsync(TEntity entity, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        EntityEntry<TEntity> FakeDelete(object key);

        /// <summary>
        /// 假删除
        /// </summary>
        /// <param name="key">主键</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteAsync(object key);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        EntityEntry<TEntity> FakeDeleteNow(object key);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <returns></returns>
        EntityEntry<TEntity> FakeDeleteNow(object key, bool acceptAllChangesOnSuccess);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteNowAsync(object key, CancellationToken cancellationToken = default);

        /// <summary>
        /// 假删除并立即提交
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="acceptAllChangesOnSuccess">接受所有更改</param>
        /// <param name="cancellationToken">异步取消令牌</param>
        /// <returns></returns>
        Task<EntityEntry<TEntity>> FakeDeleteNowAsync(object key, bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);
    }
}