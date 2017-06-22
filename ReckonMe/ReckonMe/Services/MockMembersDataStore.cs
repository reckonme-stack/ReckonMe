﻿using ReckonMe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(ReckonMe.Services.MockMembersDataStore))]
namespace ReckonMe.Services
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="ReckonMe.Services.IDataStore{ReckonMe.Models.Member}" />
    class MockMembersDataStore : IDataStore<Member>
    {
        /// <summary>
        /// The is initialized
        /// </summary>
        private bool _isInitialized;
        /// <summary>
        /// The items
        /// </summary>
        private List<Member> _items;

        /// <summary>
        /// Adds the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> AddItemAsync(Member item)
        {
            await InitializeAsync();

            _items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Updates the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> UpdateItemAsync(Member item)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(_item);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Deletes the item asynchronous.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        public async Task<bool> DeleteItemAsync(Member item)
        {
            await InitializeAsync();

            var _item = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(_item);

            return await Task.FromResult(true);
        }

        /// <summary>
        /// Gets the item asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<Member> GetItemAsync(string id)
        {
            await InitializeAsync();

            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        /// <summary>
        /// Gets the items asynchronous.
        /// </summary>
        /// <param name="forceRefresh">if set to <c>true</c> [force refresh].</param>
        /// <returns></returns>
        public async Task<IEnumerable<Member>> GetItemsAsync(bool forceRefresh = false)
        {
            await InitializeAsync();

            return await Task.FromResult(_items);
        }

        /// <summary>
        /// Pulls the latest asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<bool> PullLatestAsync()
        {
            return Task.FromResult(true);
        }


        /// <summary>
        /// Synchronizes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public Task<bool> SyncAsync()
        {
            return Task.FromResult(true);
        }

        /// <summary>
        /// Initializes the asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;

            _items = new List<Member>();

            var mockedMembers = new List<Member>
            {
                new Member { Id = Guid.NewGuid().ToString(), Name = "dkobierski"}
            };

            foreach (var item in mockedMembers)
            {
                _items.Add(item);
            }

            _isInitialized = true;
        }
    }
}
