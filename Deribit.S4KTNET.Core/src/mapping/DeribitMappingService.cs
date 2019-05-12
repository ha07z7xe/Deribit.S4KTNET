using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Deribit.S4KTNET.Core.Mapping
{
    internal class DeribitMappingService
    {

        //------------------------------------------------------------------------------------------------
        // module
        //------------------------------------------------------------------------------------------------

        internal class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder builder)
            {
                builder.RegisterType<DeribitMappingService>()
                    .AsSelf()
                    .SingleInstance();

                // build automapper configuration
                var automapperconfig = new MapperConfiguration(cfg =>
                {
                    cfg.AddMaps(GetType());
                });
                automapperconfig.AssertConfigurationIsValid();
                automapperconfig.CompileMappings();

                // build mapper
                var mapper = automapperconfig.CreateMapper();
                
                // register
                builder.RegisterInstance(automapperconfig);
                builder.RegisterInstance(mapper);
            }
        }
        //------------------------------------------------------------------------------------------------
    }
}
