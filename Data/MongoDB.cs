using System;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using EscalasADCWebAPI.Data.Collections;

namespace EscalasADCWebAPI.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }
        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings = MongoClientSettings.FromUrl(new MongoUrl(configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            }
            catch (Exception ex)
            {
                throw new MongoClientException("Não foi possível conectar ao MongoDB", ex);
            }
        }
        private void MapClasses()
        {
            var ConventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
            ConventionRegistry.Register("camelCase", ConventionPack, t => true);
            if (!BsonClassMap.IsClassMapRegistered(typeof(Voluntario)))
            {
                BsonClassMap.RegisterClassMap<Voluntario>(i =>
                {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }



        }
    }
}