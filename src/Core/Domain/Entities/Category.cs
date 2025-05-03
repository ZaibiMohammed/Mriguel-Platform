using System;
using System.Collections.Generic;
using Mriguel.Domain.Common;

namespace Mriguel.Domain.Entities
{
    /// <summary>
    /// Represents a category for items
    /// </summary>
    public class Category : AuditableEntity
    {
        private readonly List<ItemCategory> _itemCategories = new();
        
        private Category() { } // Required by EF Core
        
        public Category(string name, string description = null, Category parent = null)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description;
            
            if (parent != null)
            {
                ParentId = parent.Id;
                Parent = parent;
            }
        }
        
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string IconUrl { get; private set; }
        public Guid? ParentId { get; private set; }
        public Category Parent { get; private set; }
        public IReadOnlyCollection<ItemCategory> ItemCategories => _itemCategories.AsReadOnly();
        
        public void UpdateName(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
        
        public void UpdateDescription(string description)
        {
            Description = description;
        }
        
        public void UpdateIconUrl(string iconUrl)
        {
            IconUrl = iconUrl;
        }
    }
}
