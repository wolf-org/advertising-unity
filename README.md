<p align="left">
  <a>
    <img alt="Made With Unity" src="https://img.shields.io/badge/made%20with-Unity-57b9d3.svg?logo=Unity">
  </a>
  <a>
    <img alt="License" src="https://img.shields.io/github/license/wolf-package/advertising?logo=github">
  </a>
  <a>
    <img alt="Last Commit" src="https://img.shields.io/github/last-commit/wolf-package/advertising?logo=Mapbox&color=orange">
  </a>
  <a>
    <img alt="Repo Size" src="https://img.shields.io/github/repo-size/wolf-package/advertising?logo=VirtualBox">
  </a>
  <a>
    <img alt="Last Release" src="https://img.shields.io/github/v/release/wolf-package/advertising?include_prereleases&logo=Dropbox&color=yellow">
  </a>
</p>

## What
### Show ads tool for unity games (support for Max-Applovin and Google Mobile Ads)

## How To Install

### Add the line below to `Packages/manifest.json`

for version `1.0.4`
```csharp
"com.wolf-package.advertising":"https://github.com/wolf-package/advertising.git#1.0.4",
```

## Use


- Import `Scripting Define Symbols` in `Project Settings` > `Player` > `Other Settings`
    - Applovin: `VIRTUESKY_ADS` and `VIRTUESKY_MAX`
    - Admob: `VIRTUESKY_ADS` and `VIRTUESKY_ADMOB`
    - Tracking revenue by Adjust: `VIRTUESKY_ADJUST`
    - Tracking revenue by Firebase Analytic: `VIRTUESKY_FIREBASE_ANALYTIC`
    - Tracking revenue by AppsFlyer: `VIRTUESKY_APPSFLYER`

