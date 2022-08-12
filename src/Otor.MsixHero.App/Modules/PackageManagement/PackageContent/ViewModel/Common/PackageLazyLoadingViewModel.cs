﻿// MSIX Hero
// Copyright (C) 2022 Marcin Otorowski
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// Full notice:
// https://github.com/marcinotorowski/msix-hero/blob/develop/LICENSE.md

using System.Threading;
using System.Threading.Tasks;
using Otor.MsixHero.App.Modules.PackageManagement.PackageContent.Enums;
using Otor.MsixHero.App.Mvvm;
using Otor.MsixHero.Appx.Packaging.Manifest.Entities;

namespace Otor.MsixHero.App.Modules.PackageManagement.PackageContent.ViewModel.Common;

public abstract class PackageLazyLoadingViewModel : NotifyPropertyChanged, IPackageContentItem, ILoadPackage
{
    private bool _isActive;
    private (AppxPackage, string) _pending;

    public abstract PackageContentViewType Type { get; }

    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (!SetField(ref _isActive, value))
            {
                return;
            }

            if (value && _pending != default)
            {
                DoLoadPackage(_pending.Item1, _pending.Item2, CancellationToken.None);
                _pending = default;
            }
        }
    }

    public Task LoadPackage(AppxPackage model, string filePath, CancellationToken cancellationToken)
    {
        if (IsActive)
        {
            _pending = default;
            return DoLoadPackage(model, filePath, cancellationToken);
        }

        _pending = new(model, filePath);
        return Task.CompletedTask;
    }

    protected abstract Task DoLoadPackage(AppxPackage model, string filePath, CancellationToken cancellationToken);
}