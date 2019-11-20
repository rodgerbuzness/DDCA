using DDCA.Models;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDCA
{
    public class Database
    {
        private const string SessionKey = "DDCA.Database.SessionKey";

        private static ISessionFactory _sessionFactory;

        public static ISession Session
        {
            get { return (ISession)HttpContext.Current.Items[SessionKey]; }
        }
        public static void Configure()
        {
            var config = new Configuration();

            //configure connection string

            //add our mappings
            var mapper = new ModelMapper();
            mapper.AddMapping<UserMap>();
            mapper.AddMapping<RoleMap>();
            mapper.AddMapping<ClientMap>();
            mapper.AddMapping<GeosurveyMap>();
            mapper.AddMapping<SurveyProfileMap>();
            mapper.AddMapping<StaffMap>();
            mapper.AddMapping<RegionMap>();
            mapper.AddMapping<DistrictMap>();
            mapper.AddMapping<BoreholeMap>();
            mapper.AddMapping<BoreStrataMap>();
            mapper.AddMapping<PumpTestMap>();
            mapper.AddMapping<LabAnalysisMap>();
            mapper.AddMapping<ChemicalMap>();
            mapper.AddMapping<PhysicalMap>();
            mapper.AddMapping<BoreDrillMethodMap>();
            mapper.AddMapping<DrillingTypeMap>();
            mapper.AddMapping<DrillMethodMapp>();
            mapper.AddMapping<RigMap>();
            mapper.AddMapping<CarMap>();
            mapper.AddMapping<CompressorMap>();
            mapper.AddMapping<MachineMap>();
            mapper.AddMapping<MachineServiceMap>();

            config.AddMapping(mapper.CompileMappingForAllExplicitlyAddedEntities());

            //create session factory
            _sessionFactory = config.BuildSessionFactory();
        }

        public static void OpenSession()
        {
            HttpContext.Current.Items[SessionKey] = _sessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            var session = HttpContext.Current.Items[SessionKey] as ISession;
            if (session != null)
                session.Close();

            HttpContext.Current.Items.Remove(SessionKey);
        }
    }
}