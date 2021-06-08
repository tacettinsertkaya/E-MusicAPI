using EMusicAPI.Entity;
using EMusicAPI.Helper;
using EMusicAPI.Models.DataFilter;
using EMusicAPI.Models.DataFilter.Music;
using EMusicAPI.Models.Dto;
using EMusicAPI.Models.Wrappers;
using EMusicAPI.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMusicAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private IMusicService _musicService { get; set; }
        private readonly IMemoryCache _memCache;


        public MusicController(IMusicService MusicService,
                              IMemoryCache memCache)
        {
            _musicService = MusicService;
            _memCache = memCache;

        }
        // POST: api/<MusicController>

        [HttpPost("GetListByFilterAsync")]
        public async Task<IActionResult> GetListByFilterAsync([FromBody] MusicListFilter filter)
        {
            const string cacheKey = "musicList";

            if(_memCache.TryGetValue(cacheKey, out object list))
               return Ok(list);

            var musicList = await _musicService.GetListByFilterAsync(filter);

            _memCache.Set(cacheKey, musicList, new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddSeconds(20),
                Priority = CacheItemPriority.Normal
            });
            return Ok(musicList);

        }

        
        [HttpPost("GetUserMusicListByFilterAsync")]
        public async Task<IActionResult> GetUserMusicListByFilterAsync([FromBody] MusicListFilter filter)
        {


            //const string cacheKey = "userMusicList";

            //if (_memCache.TryGetValue(cacheKey, out object list))
            //    return Ok(list);

            var userMusicList = await _musicService.GetUserMusicListByFilterAsync(filter);

            //_memCache.Set(cacheKey, userMusicList, new MemoryCacheEntryOptions
            //{
            //    AbsoluteExpiration = DateTime.Now.AddSeconds(20),
            //    Priority = CacheItemPriority.Normal
            //});
            return Ok(userMusicList);


        }



        // POST api/<MusicController>
        [HttpPost("GetByFilterAsync")]
        public async Task<IActionResult> GetByFilterAsync(MusicFilter filter)
        {
            var response = await _musicService.GetByFilterAsync(filter);

            return Ok(response);
        }


        // POST api/<MusicController>
        [HttpPost("GetUserMusicByFilterAsync")]
        public async Task<IActionResult> GetUserMusicByFilterAsync(MusicFilter filter)
        {
            var response = await _musicService.GetUserMusicByFilterAsync(filter);

            return Ok(response);
        }

        [Authorize]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Music Music)
        {
            var response = await _musicService.CreateAsync(Music);
            return Ok(response);
        }

        [Authorize]
        // PUT api/<MusicController>/5
        [HttpPost("Update")]
        public async Task<IActionResult> Update([FromBody] Music Music)
        {
            var response = await _musicService.UpdateAsync(Music);
            return Ok(response);

        }

        [Authorize]
        [HttpPost("StateUserMusic")]
        public async Task<IActionResult> StateUserMusic([FromBody] UserMusicDto userMusic)
        {
            var response = await _musicService.StateUserMusic(userMusic);
            return Ok(response);

        }


        [Authorize]
        [HttpPost("BuyMusic")]
        public async Task<IActionResult> BuyMusic([FromBody] UserMusicDto userMusic)
        {
            var response = await _musicService.BuyMusic(userMusic);
            return Ok(response);

        }

        [Authorize]
        // DELETE api/<MusicController>/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _musicService.DeleteAsync(id);
            return Ok(response);
        }
    }
}
