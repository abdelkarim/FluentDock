// Copyright (c) 2014 'Abdelkarim Sellamna' (abdelkarim.se [at] gmail [dot] com)
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace FluentDock.Model
{
    public abstract class DockingView : View
    {
        public ItemLength DockedWidth { get; set; }
        public ItemLength DockedHeight { get; set; }
        public ItemLength FloatingWidth { get; set; }
        public ItemLength FloatingHeight { get; set; }
    }
}