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

using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace Furion.DatabaseAccessor
{
    /// <summary>
    /// 数据库上下文提交拦截器
    /// </summary>
    [SkipScan]
    public class DbContextSaveChangesInterceptor : SaveChangesInterceptor
    {
        /// <summary>
        /// 拦截保存数据库之前
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavingChangesEventInner(eventData, result);

            return base.SavingChanges(eventData, result);
        }

        /// <summary>
        /// 拦截保存数据库之前
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavingChangesEventInner(eventData, result);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// 拦截保存数据库成功
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavedChangesEventInner(eventData, result);

            return base.SavedChanges(eventData, result);
        }

        /// <summary>
        /// 拦截保存数据库成功
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="result"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SavedChangesEventInner(eventData, result);

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        /// <summary>
        /// 拦截保存数据库失败
        /// </summary>
        /// <param name="eventData"></param>
        public override void SaveChangesFailed(DbContextErrorEventData eventData)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SaveChangesFailedEventInner(eventData);

            base.SaveChangesFailed(eventData);
        }

        /// <summary>
        /// 拦截保存数据库失败
        /// </summary>
        /// <param name="eventData"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task SaveChangesFailedAsync(DbContextErrorEventData eventData, CancellationToken cancellationToken = default)
        {
            dynamic dbContext = eventData.Context;
            dbContext.SaveChangesFailedEventInner(eventData);

            return base.SaveChangesFailedAsync(eventData, cancellationToken);
        }
    }
}