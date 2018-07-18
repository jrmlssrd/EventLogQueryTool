using System;
using System.ComponentModel;
using System.Resources;

namespace EventLogQueryToolCore.Common
{
    public class LocalizedDescriptionAttribute : DescriptionAttribute
    {
        #region Private Fields

        private string _resourceKey;
        private ResourceManager _resourceManager;

        #endregion Private Fields

        #region Public Constructors

        public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
        {
            _resourceManager = new ResourceManager(resourceType);
            _resourceKey = resourceKey;
        }

        #endregion Public Constructors

        #region Public Properties

        public override string Description
        {
            get
            {
                string description = _resourceManager.GetString(_resourceKey);
                return string.IsNullOrWhiteSpace(description) ? string.Format("{0}", _resourceKey) : description;
            }
        }

        #endregion Public Properties
    }
}