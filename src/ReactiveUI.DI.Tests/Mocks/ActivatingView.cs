﻿// Copyright (c) 2021 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Subjects;

namespace ReactiveUI.DI.Tests.Mocks
{
    /// <summary>
    /// Activating View.
    /// </summary>
    /// <seealso cref="ReactiveUI.ReactiveObject" />
    /// <seealso cref="ReactiveUI.IActivatableView" />
    public sealed class ActivatingView : ReactiveObject, IViewFor<ActivatingViewModel>, IDisposable
    {
        private int _count;
        private ActivatingViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActivatingView"/> class.
        /// </summary>
        public ActivatingView()
        {
            this.WhenActivated(d =>
            {
                _count++;
                d(Disposable.Create(() => _count--));
            });
        }

        /// <summary>
        /// Gets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int IsActiveCount => _count;

        /// <summary>
        /// Gets the loaded.
        /// </summary>
        public Subject<Unit> Loaded { get; } = new();

        /// <summary>
        /// Gets the unloaded.
        /// </summary>
        public Subject<Unit> Unloaded { get; } = new();

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        public ActivatingViewModel ViewModel
        {
            get => _viewModel;
            set => this.RaiseAndSetIfChanged(ref _viewModel, value);
        }

        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        object IViewFor.ViewModel
        {
            get => ViewModel;
            set => ViewModel = (ActivatingViewModel)value;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Loaded.Dispose();
            Unloaded.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
