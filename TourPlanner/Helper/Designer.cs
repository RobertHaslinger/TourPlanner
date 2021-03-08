using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TourPlanner.Helper
{
    public static class Designer
    {
        #region IsDesignMode

        private static readonly bool _isDesignMode;

        /// <summary>
        /// Checks whether the application is currently in design mode.
        /// </summary>
        public static bool IsDesignMode
        {
            get { return _isDesignMode; }
        }

        #endregion

        static Designer()
        {
            DependencyProperty prop = DesignerProperties.IsInDesignModeProperty;
            _isDesignMode = (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement))
                .Metadata.DefaultValue;

        }
    }
}
