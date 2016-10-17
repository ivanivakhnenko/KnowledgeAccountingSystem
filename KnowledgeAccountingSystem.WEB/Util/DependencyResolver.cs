using System;
using System.Collections.Generic;
using System.Web.Mvc;
using KnowledgeAccountingSystem.BLL.Interfaces;
using KnowledgeAccountingSystem.BLL.Services;
using Ninject;

namespace KnowledgeAccountingSystem.WEB.Util
{
    public class DependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;
        public DependencyResolver(IKernel kernelParam)
        {
            _kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            _kernel.Bind<IUserService>().To<UserService>();
            _kernel.Bind<ITeamUpService>().To<TeamUpService>();
            _kernel.Bind<IEvaluateService>().To<EvaluateService>();
            _kernel.Bind<ISkillsService>().To<SkillsService>();
        }
    }
}