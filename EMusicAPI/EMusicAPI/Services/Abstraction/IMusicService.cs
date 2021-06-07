using EMusicAPI.Entity;
using EMusicAPI.Models.DataFilter;
using EMusicAPI.Models.DataFilter.Music;
using EMusicAPI.Models.Dto;
using EMusicAPI.Models.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMusicAPI.Services.Abstraction
{
    public interface IMusicService
    {
        Task<Response<Music>> CreateAsync(Music Music);
        Task<Response<bool>> DeleteAsync(Guid Id);
        Task<Response<Music>> GetByFilterAsync(MusicFilter filter);
        Task<Response<UserMusicDto>> GetUserMusicByFilterAsync(MusicFilter filter);
        Task<PagedResponse<List<Music>>> GetListByFilterAsync(MusicListFilter filter);
        Task<PagedResponse<List<UserMusicDto>>> GetUserMusicListByFilterAsync(MusicListFilter filter);
        Task<Response<Music>> UpdateAsync(Music music);
        Task<Response<UserMusic>> StateUserMusic(UserMusicDto userMusic);
        Task<Response<UserMusic>> AddUserMusic(UserMusic userMusic);
        Task<Response<UserMusic>> UpdateUserMusic(UserMusic userMusic);
        Task<Response<UserMusic>> BuyMusic(UserMusicDto userMusic);

    }
}
