﻿using System.Threading.Tasks;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Services.Ads;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Windows;
using CodeBase.UI.Elements;
using CodeBase.UI.Services.Windows;
using CodeBase.UI.Windows.Shop;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Services.Factory
{
  public class UIFactory : IUIFactory
  {
    private const string UIRootPath = "UIRoot";
    private readonly IAssetProvider _assets;
    private readonly IStaticDataService _staticData;
    private readonly DiContainer _container;
    private readonly IPersistentProgressService _progressService;
    private readonly IAdsService _adsService;

    private Transform _uiRoot;

    public UIFactory(IAssetProvider assets, IStaticDataService staticData, IPersistentProgressService progressService,
      IAdsService adsService, DiContainer container)
    {
      _container = container;
      _assets = assets;
      _staticData = staticData;
      _progressService = progressService;
      _adsService = adsService;
    }

    public void CreateShop()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.Shop);
      ShopWindow window = Object.Instantiate(config.Template, _uiRoot) as ShopWindow;
      window.Construct(_adsService,_progressService);
    }

    public async Task CreateUIRoot()
    {
      GameObject root = await _assets.Instantiate(UIRootPath);
      _uiRoot = root.transform;
    }
    
    public void CreateMainMenu()
    {
      WindowConfig config = _staticData.ForWindow(WindowId.MainMenu);
      MainMenu window = Object.Instantiate(config.Template, _uiRoot) as MainMenu;
      window.Construct(_progressService);

      foreach (OpenWindowButton openWindowButton in window.GetComponentsInChildren<OpenWindowButton>())
        openWindowButton.Init(_container.Resolve<IWindowService>());

    }
  }
}