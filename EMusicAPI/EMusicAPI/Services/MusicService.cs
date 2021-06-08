using EMusicAPI.Context;
using EMusicAPI.Entity;
using EMusicAPI.Helper;
using EMusicAPI.Models.DataFilter;
using EMusicAPI.Models.DataFilter.Music;
using EMusicAPI.Models.Dto;
using EMusicAPI.Models.Wrappers;
using EMusicAPI.Services.Abstraction;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EMusicAPI.Services
{
    public class MusicService : IMusicService
    {
        private EMusicDbContext _db { get; set; }
        private ApplicationDbContext _userdb { get; set; }
        private IQueueService _queueService { get; set; }

        public MusicService(EMusicDbContext db,
                            ApplicationDbContext userdb,
                            IQueueService queueService
                            )
        {
            _db = db;
            _userdb = userdb;
            _queueService = queueService;
        }
        public async Task<Response<Music>> CreateAsync(Music Music)
        {
            _db.Musics.Add(Music);
            await _db.SaveChangesAsync();


            return new Response<Music>(Music);
        }

        public async Task<Response<bool>> DeleteAsync(Guid Id)
        {
            var Music = await _db.Musics.Where(p => p.Id == Id).FirstOrDefaultAsync();
            var result = _db.Musics.Remove(Music);
            await _db.SaveChangesAsync();



            return new Response<bool>(true);
        }

        public async Task<Response<Music>> GetByFilterAsync(MusicFilter filter)
        {


            IQueryable<Music> query = _db.Musics;

            if (!string.IsNullOrEmpty(filter.Id))
                query = query.Where(p => p.Id == Guid.Parse(filter.Id));
            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            var response = await query.FirstOrDefaultAsync();

            if (!(response is null))
            {
                var existUserMusic = await _db.UserMusic.Where(p => p.MusicId == response.Id).FirstOrDefaultAsync();
                if (!(existUserMusic is null) && !existUserMusic.IsViewed)
                {
                    existUserMusic.IsViewed = true;
                    _db.UserMusic.Update(existUserMusic);

                }
                else
                {
                    var data = new UserMusic();
                    data.MusicId = response.Id;
                    data.IsViewed = true;

                    _db.UserMusic.Add(data);

                }

                _db.SaveChanges();
            }

            return new Response<Music>(response);
        }


        public async Task<Response<UserMusicDto>> GetUserMusicByFilterAsync(MusicFilter filter)
        {


            IQueryable<UserMusic> query = _db.UserMusic;
            var response = new UserMusicDto();
            if (!string.IsNullOrEmpty(filter.Id))
                query = query.Where(p => p.MusicId == Guid.Parse(filter.Id));

            var data = await query.Include(p => p.Music).FirstOrDefaultAsync();
            if (!(data is null))
            {

                response.Music = data.Music;
                response.IsFavorite = data.IsFavorite;
                response.IsPurchashing = data.IsPurchashing;
                response.IsViewed = data.IsViewed;

                return new Response<UserMusicDto>(response);
            }
            return new Response<UserMusicDto>(response);

        }

        public async Task<PagedResponse<List<Music>>> GetListByFilterAsync(MusicListFilter filter)
        {

            IQueryable<Music> query = _db.Musics;


            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));
            
            if (!string.IsNullOrEmpty(filter.OwnerFullName))
                query = query.Where(p => p.OwnerFullName.Contains(filter.OwnerFullName));
            
            if (!string.IsNullOrEmpty(filter.SearchParams))
                query = query.Where(p => p.Name.Contains(filter.SearchParams) ||
                                         p.OwnerFullName.Contains(filter.SearchParams)                                          
                                         );


            var pagedData = await query.OrderByDescending(p => p.Name)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize)
                            .ToListAsync();
            var totalRecords = await query.CountAsync();

            var pagedReponse = PaginationHelper.CreatePagedReponse<Music>(pagedData, filter, totalRecords, null, "");
            return pagedReponse;

        }

        public async Task<PagedResponse<List<UserMusicDto>>> GetUserMusicListByFilterAsync(MusicListFilter filter)
        {
            IQueryable<Music> query = _db.Musics;

            if (!string.IsNullOrEmpty(filter.Name))
                query = query.Where(p => p.Name.Contains(filter.Name));

            if (!string.IsNullOrEmpty(filter.OwnerFullName))
                query = query.Where(p => p.OwnerFullName.Contains(filter.OwnerFullName));

            if (!string.IsNullOrEmpty(filter.SearchParams))
                query = query.Where(p => p.Name.Contains(filter.SearchParams) ||
                                         p.OwnerFullName.Contains(filter.SearchParams)
                                         );

            var pagedData = await query.OrderByDescending(p => p.Name)
                            .Skip((filter.PageNumber - 1) * filter.PageSize)
                            .Take(filter.PageSize).Select(p => new UserMusicDto()
                            {
                                Music = p
                            })
                            .ToListAsync();
            var totalRecords = await query.CountAsync();

            var userMusicList = await _db.UserMusic.ToListAsync();
            var userMusic = userMusicList.Where(p => pagedData.Any(x => x.Music.Id == p.MusicId)).ToList();

            pagedData.ForEach(p =>
            {
                var data = userMusic.Where(k => k.Music.Id == p.Music.Id).FirstOrDefault();
                if (!(data is null))
                {
                    p.IsViewed = data.IsViewed;
                    p.IsPurchashing = data.IsPurchashing;
                    p.IsFavorite = data.IsFavorite;
                }

            });

            var pagedReponse = PaginationHelper.CreatePagedReponse<UserMusicDto>(pagedData, filter, totalRecords, null, "");
            return pagedReponse;

        }

        public async Task<Response<Music>> UpdateAsync(Music Music)
        {
            try
            {
                var existMusic = await _db.Musics.Where(p => p.Id == Music.Id).FirstOrDefaultAsync();
                if (existMusic != null)
                {

                    _db.Entry(existMusic).CurrentValues.SetValues(Music);

                    await _db.SaveChangesAsync();
                    return new Response<Music>(existMusic);
                }
                return new Response<Music>(Music);
            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task<Response<UserMusic>> BuyMusic(UserMusicDto userMusic)
        {

            var response=await StateUserMusic(userMusic);

            if (!(response is null))
            {
                var notif = new Notif();
                notif.Title = "Notif";
                notif.Content = "The purchase was succeed";
                _queueService.SendNotifQueue(notif);
                //_queueService.GetNotifQueue();
                
            }
            return response;
        }


        public async Task<Response<UserMusic>> StateUserMusic(UserMusicDto userMusic)
        {
            try
            {
                var existMusic = await _db.UserMusic.Where(p => p.MusicId == userMusic.Music.Id).FirstOrDefaultAsync();

                var data = new UserMusic();
                data.MusicId = userMusic.Music.Id;
                data.IsFavorite = userMusic.IsFavorite;
                data.IsPurchashing = userMusic.IsPurchashing;
                data.IsViewed = userMusic.IsViewed;

                if (existMusic is null)
                    return await AddUserMusic(data);
                else
                    return await UpdateUserMusic(data);


            }
            catch (Exception ex)
            {
                throw;
            }


        }


        public async Task<Response<UserMusic>> AddUserMusic(UserMusic userMusic)
        {
            try
            {
                if (!(userMusic is null))
                {

                    _db.UserMusic.Add(userMusic);

                    await _db.SaveChangesAsync();
                    return new Response<UserMusic>(userMusic);
                }
                else
                {
                    return new Response<UserMusic>()
                    {
                        Succeeded = false,
                        Message = "Model is empty"
                    };
                }

            }
            catch (Exception ex)
            {
                throw;
            }


        }

        public async Task<Response<UserMusic>> UpdateUserMusic(UserMusic userMusic)
        {
            try
            {
                var existMusic = await _db.UserMusic.Where(p => p.MusicId == userMusic.MusicId).FirstOrDefaultAsync();
                if (existMusic != null)
                {
                    existMusic.IsViewed = userMusic.IsViewed;
                    existMusic.IsFavorite = userMusic.IsFavorite;
                    existMusic.IsPurchashing = userMusic.IsPurchashing;
                    _db.UserMusic.Update(existMusic);

                    await _db.SaveChangesAsync();
                    return new Response<UserMusic>(userMusic);
                }

                return new Response<UserMusic>(userMusic);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
