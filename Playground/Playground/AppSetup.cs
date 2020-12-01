﻿using Playground.Data.Infrastructure;
using Playground.Data.Repositories;
using Playground.Features.Animation;
using Playground.Features.BattleTest;
using Playground.Features.CssPreviewer;
using Playground.Features.Editor;
using Playground.Features.Gallery;
using Playground.Features.Gallery.Services;
using Playground.Features.Home;
using Playground.Features.Linear;
using Playground.Features.Radial;
using SimpleInjector;
using Xamarin.Forms;
using GradientEditorPage = Playground.Features.Editor.GradientEditorPage;

namespace Playground
{
    public static class AppSetup
    {
        public static Container IoC { get; } = new Container();

        public static void RegisterTypes()
        {
            IoC.Register<IDatabaseProvider, DatabaseProvider>();
            IoC.Register<IDatabaseUpdater, DatabaseUpdater>();

            IoC.Register<IDocumentRepository, DocumentRepository>();
            IoC.Register<IGradientRepository, GradientRepository>();
            IoC.Register<ICategoryRepository, CategoryRepository>();

            IoC.Register<IGalleryService, GalleryService>();
            IoC.Register<ICategoryService, CategoryService>();
            IoC.Register<IBattleItemService, BattleItemService>();
        }

        public static void RegisterViewModels()
        {
            IoC.Register<HomeViewModel>();
            IoC.Register<GradientEditorViewModel>();
            IoC.Register<LinearSamplesViewModel>();
            IoC.Register<GalleryCategoriesViewModel>();
            IoC.Register<GalleryListViewModel>();
            IoC.Register<CssPreviewerViewModel>();
            IoC.Register<BattleTestViewModel>();
            IoC.Register<AnimationsViewModel>();
        }

        public static void RegisterRoutes()
        {
            Routing.RegisterRoute("GradientEditor", typeof(GradientEditorPage));
            Routing.RegisterRoute("Gallery", typeof(GalleryCategoriesPage));
            Routing.RegisterRoute("GalleryList", typeof(GalleryListPage));
            Routing.RegisterRoute("CssPreviewer", typeof(CssPreviewerPage));
            Routing.RegisterRoute("Animations", typeof(AnimationsPage));
            Routing.RegisterRoute("BattleTest", typeof(BattleTestPage));
        }

        public static void Initialize()
        {
            IoC.GetInstance<IDocumentRepository>().SetupMapper();

            var repositories = new ICanUpdateMyself[]
            {
                IoC.GetInstance<IGradientRepository>(),
                IoC.GetInstance<ICategoryRepository>()
            };

            IoC.GetInstance<IDatabaseUpdater>().RunUpdate(repositories);
        }
    }
}