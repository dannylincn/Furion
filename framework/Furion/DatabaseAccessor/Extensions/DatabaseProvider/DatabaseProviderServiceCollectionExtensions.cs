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

using Furion.DatabaseAccessor;
using Furion.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Sqlite 数据库服务拓展
    /// </summary>
    [SkipScan]
    public static class DatabaseProviderServiceCollectionExtensions
    {
        /// <summary>
        /// 添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services, string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null, string connectionString = default, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册数据库上下文
            return services.AddDbPool<TDbContext, MasterDbContextLocator>(providerName, optionBuilder, connectionString, poolSize, interceptors);
        }

        /// <summary>
        /// 添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册数据库上下文
            return services.AddDbPool<TDbContext, MasterDbContextLocator>(optionBuilder, poolSize, interceptors);
        }

        /// <summary>
        /// 添加其他数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services, string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null, string connectionString = default, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            var connStr = DbProvider.GetConnectionString<TDbContext>(connectionString);
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(options =>
            {
                var _options = ConfigureDatabase<TDbContext>(providerName, connStr, options);
                optionBuilder?.Invoke(_options);
            }, interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        /// 添加其他数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="poolSize">池大小</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDbPool<TDbContext, TDbContextLocator>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, int poolSize = 100, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            services.AddDbContextPool<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors), poolSize: poolSize);

            return services;
        }

        /// <summary>
        ///  添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services, string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null, string connectionString = default, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册数据库上下文
            return services.AddDb<TDbContext, MasterDbContextLocator>(providerName, optionBuilder, connectionString, interceptors);
        }

        /// <summary>
        ///  添加默认数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
            where TDbContext : DbContext
        {
            // 避免重复注册默认数据库上下文
            if (Penetrates.DbContextWithLocatorCached.ContainsKey(typeof(MasterDbContextLocator))) throw new InvalidOperationException("Prevent duplicate registration of default DbContext.");

            // 注册数据库上下文
            return services.AddDb<TDbContext, MasterDbContextLocator>(optionBuilder, interceptors);
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="optionBuilder"></param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services, string providerName = default, Action<DbContextOptionsBuilder> optionBuilder = null, string connectionString = default, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            var connStr = DbProvider.GetConnectionString<TDbContext>(connectionString);
            services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(options =>
            {
                var _options = ConfigureDatabase<TDbContext>(providerName, connStr, options);
                optionBuilder?.Invoke(_options);
            }, interceptors));

            return services;
        }

        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <typeparam name="TDbContext">数据库上下文</typeparam>
        /// <typeparam name="TDbContextLocator">数据库上下文定位器</typeparam>
        /// <param name="services">服务</param>
        /// <param name="optionBuilder">自定义配置</param>
        /// <param name="interceptors">拦截器</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDb<TDbContext, TDbContextLocator>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionBuilder, params IInterceptor[] interceptors)
            where TDbContext : DbContext
            where TDbContextLocator : class, IDbContextLocator
        {
            // 注册数据库上下文
            services.RegisterDbContext<TDbContext, TDbContextLocator>();

            // 配置数据库上下文
            services.AddDbContext<TDbContext>(Penetrates.ConfigureDbContext(optionBuilder, interceptors));

            return services;
        }

        /// <summary>
        /// 配置数据库
        /// </summary>
        /// <typeparam name="TDbContext"></typeparam>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="connectionString">数据库连接字符串</param>
        /// <param name="options">数据库上下文选项构建器</param>
        private static DbContextOptionsBuilder ConfigureDatabase<TDbContext>(string providerName, string connectionString, DbContextOptionsBuilder options)
             where TDbContext : DbContext
        {
            var dbContextOptionsBuilder = options;

            // 获取数据库上下文特性
            var dbContextAttribute = DbProvider.GetAppDbContextAttribute(typeof(TDbContext));
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                providerName ??= dbContextAttribute?.ProviderName;

                // 解析数据库提供器信息
                (var name, var version) = ReadProviderInfo(providerName);
                providerName = name;

                // 调用对应数据库程序集
                var (UseMethod, MySqlVersion) = GetDatabaseProviderUseMethod(providerName, version);

                // 处理最新第三方 MySql 包兼容问题
                // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
                if (DbProvider.IsDatabaseFor(providerName, DbProvider.MySql))
                {
                    dbContextOptionsBuilder = UseMethod
                        .Invoke(null, new object[] { options, connectionString, MySqlVersion, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
                }
                // 处理 SqlServer 2005-2008 兼容问题
                else if (DbProvider.IsDatabaseFor(providerName, DbProvider.SqlServer) && (version == "2008" || version == "2005"))
                {
                    // 替换工厂
                    dbContextOptionsBuilder.ReplaceService<IQueryTranslationPostprocessorFactory, SqlServer2008QueryTranslationPostprocessorFactory>();

                    dbContextOptionsBuilder = UseMethod
                        .Invoke(null, new object[] { options, connectionString, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
                }
                // 处理 Oracle 11 兼容问题
                else if (DbProvider.IsDatabaseFor(providerName, DbProvider.Oracle) && !string.IsNullOrWhiteSpace(version))
                {
                    Action<IRelationalDbContextOptionsBuilderInfrastructure> oracleOptionsAction = options =>
                    {
                        var optionsType = options.GetType();

                        // 处理版本号
                        optionsType.GetMethod("UseOracleSQLCompatibility")
                                   .Invoke(options, new[] { version });

                        // 处理迁移程序集
                        optionsType.GetMethod("MigrationsAssembly")
                                   .Invoke(options, new[] { Db.MigrationAssemblyName });
                    };

                    dbContextOptionsBuilder = UseMethod
                        .Invoke(null, new object[] { options, connectionString, oracleOptionsAction }) as DbContextOptionsBuilder;
                }
                else
                {
                    dbContextOptionsBuilder = UseMethod
                        .Invoke(null, new object[] { options, connectionString, MigrationsAssemblyAction }) as DbContextOptionsBuilder;
                }
            }

            // 解决分表分库
            if (dbContextAttribute?.Mode == DbContextMode.Dynamic) dbContextOptionsBuilder
                  .ReplaceService<IModelCacheKeyFactory, DynamicModelCacheKeyFactory>();

            return dbContextOptionsBuilder;
        }

        /// <summary>
        /// 数据库提供器 UseXXX 方法缓存集合
        /// </summary>
        private static readonly ConcurrentDictionary<string, (MethodInfo, object)> DatabaseProviderUseMethodCollection;

        /// <summary>
        /// 配置Code First 程序集 Action委托
        /// </summary>
        private static readonly Action<IRelationalDbContextOptionsBuilderInfrastructure> MigrationsAssemblyAction;

        /// <summary>
        /// 静态构造方法
        /// </summary>
        static DatabaseProviderServiceCollectionExtensions()
        {
            DatabaseProviderUseMethodCollection = new ConcurrentDictionary<string, (MethodInfo, object)>();
            MigrationsAssemblyAction = options => options.GetType()
                .GetMethod("MigrationsAssembly")
                .Invoke(options, new[] { Db.MigrationAssemblyName });
        }

        /// <summary>
        /// 获取数据库提供器对应的 useXXX 方法
        /// </summary>
        /// <param name="providerName">数据库提供器</param>
        /// <param name="version"></param>
        /// <returns></returns>
        private static (MethodInfo UseMethod, object MySqlVersion) GetDatabaseProviderUseMethod(string providerName, string version)
        {
            return DatabaseProviderUseMethodCollection.GetOrAdd(providerName, Function(providerName, version));

            // 本地静态方法
            static (MethodInfo, object) Function(string providerName, string version)
            {
                // 处理最新 MySql 包兼容问题
                // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
                object mySqlVersionInstance = default;

                // 加载对应的数据库提供器程序集
                var databaseProviderAssembly = Assembly.Load(providerName);

                // 数据库提供器服务拓展类型名
                var databaseProviderServiceExtensionTypeName = providerName switch
                {
                    DbProvider.SqlServer => "SqlServerDbContextOptionsExtensions",
                    DbProvider.Sqlite => "SqliteDbContextOptionsBuilderExtensions",
                    DbProvider.Cosmos => "CosmosDbContextOptionsExtensions",
                    DbProvider.InMemoryDatabase => "InMemoryDbContextOptionsExtensions",
                    DbProvider.MySql => "MySqlDbContextOptionsBuilderExtensions",
                    DbProvider.MySqlOfficial => "MySQLDbContextOptionsExtensions",
                    DbProvider.Npgsql => "NpgsqlDbContextOptionsBuilderExtensions",
                    DbProvider.Oracle => "OracleDbContextOptionsExtensions",
                    DbProvider.Firebird => "FbDbContextOptionsBuilderExtensions",
                    DbProvider.Dm => "DmDbContextOptionsExtensions",
                    _ => null
                };

                // 加载拓展类型
                var databaseProviderServiceExtensionType = databaseProviderAssembly.GetType($"Microsoft.EntityFrameworkCore.{databaseProviderServiceExtensionTypeName}");

                // useXXX方法名
                var useMethodName = providerName switch
                {
                    DbProvider.SqlServer => $"Use{nameof(DbProvider.SqlServer)}",
                    DbProvider.Sqlite => $"Use{nameof(DbProvider.Sqlite)}",
                    DbProvider.Cosmos => $"Use{nameof(DbProvider.Cosmos)}",
                    DbProvider.InMemoryDatabase => $"Use{nameof(DbProvider.InMemoryDatabase)}",
                    DbProvider.MySql => $"Use{nameof(DbProvider.MySql)}",
                    DbProvider.MySqlOfficial => $"UseMySQL",
                    DbProvider.Npgsql => $"Use{nameof(DbProvider.Npgsql)}",
                    DbProvider.Oracle => $"Use{nameof(DbProvider.Oracle)}",
                    DbProvider.Firebird => $"Use{nameof(DbProvider.Firebird)}",
                    DbProvider.Dm => $"Use{nameof(DbProvider.Dm)}",
                    _ => null
                };

                // 获取UseXXX方法
                MethodInfo useMethod;

                // 处理最新 MySql 第三方包兼容问题
                // https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql/commit/83c699f5b747253dc1b6fa9c470f469467d77686
                if (DbProvider.IsDatabaseFor(providerName, DbProvider.MySql))
                {
                    useMethod = databaseProviderServiceExtensionType
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(u => u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 4 && u.GetParameters()[1].ParameterType == typeof(string));

                    // 解析mysql版本类型
                    var mysqlVersionType = databaseProviderAssembly.GetType("Microsoft.EntityFrameworkCore.MySqlServerVersion");
                    mySqlVersionInstance = Activator.CreateInstance(mysqlVersionType, new object[] { new Version(version ?? "8.0.22") });
                }
                else
                {
                    useMethod = databaseProviderServiceExtensionType
                        .GetMethods(BindingFlags.Public | BindingFlags.Static)
                        .FirstOrDefault(u => u.Name == useMethodName && !u.IsGenericMethod && u.GetParameters().Length == 3 && u.GetParameters()[1].ParameterType == typeof(string));
                }

                return (useMethod, mySqlVersionInstance);
            }
        }

        /// <summary>
        /// 解析数据库提供器信息
        /// </summary>
        /// <param name="providerName"></param>
        /// <returns></returns>
        private static (string name, string version) ReadProviderInfo(string providerName)
        {
            // 解析真实的数据库提供器
            var providerNameAndVersion = providerName.Split('@', StringSplitOptions.RemoveEmptyEntries);
            providerName = providerNameAndVersion.First();

            var providerVersion = providerNameAndVersion.Length > 1 ? providerNameAndVersion[1] : default;
            return (providerName, providerVersion);
        }
    }
}