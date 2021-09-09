using HomeWork.DTO.Model.ServiceDTO;
using HomeWork.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using HomeWork.Model.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HomeWork.DTO.Model.ApiDTO;

namespace HomeWork.Service.service
{
    public class FAQService : BaseService, IFAQService
    {

        public FAQService(FAQDBContext context) : base(context)
        {
        }

        /// <summary>
        /// 新增FAQ
        /// </summary>
        /// <param name="addFAQ"></param>
        /// <returns></returns>
        public async Task<bool> AddFAQ(AddFAQ addFAQ)
        {
            string upperAllId = "";
            //if (addFAQ.UpperId != null)
            //{
            //    upperAllId = GetFAQAllUpperId(addFAQ.UpperId);
            //}

            var data = new QaData
            {
                Conten = addFAQ.Conten,
                Remove = false,
                CrtTime = DateTime.Now,
                EndOn = addFAQ.EndTime,
                CrtBy = "me",
                Sort = addFAQ.Sort,
                StartOn = addFAQ.StarTime,
                Title = addFAQ.Title,
                UpperId = addFAQ.UpperId,
                UpperAllid = upperAllId
            };
            await _context.QaData.AddAsync(data);
            var add = await _context.SaveChangesAsync();
            if (add >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// 搜尋列表
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        public GetFAQAll GetByKeyWord(SearchModel searchModel)
        {
            var query = _context.QaData.Where(x => x.Remove == false).OrderBy(x => x.UpperId).AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.KeyWord))
                query = _context.QaData.Where(x =>
                    x.Conten.Contains(searchModel.KeyWord) || x.Title.Contains(searchModel.KeyWord));

            if (!(searchModel.StartOn == null))
                query = _context.QaData.Where(x =>
                    x.StartOn >= searchModel.StartOn);


            if (!(searchModel.EndOn == null))
                query = _context.QaData.Where(x =>
                    x.EndOn <= searchModel.EndOn);

            query = searchModel.OrderByName switch
            {
                "title" => searchModel.OrderBy == "DESC" ?
                    query.OrderByDescending(x => x.Title) :
                    query.OrderBy(x => x.Title),
                "conten" => searchModel.OrderBy == "DESC" ?
                    query.OrderByDescending(x => x.Conten) :
                    query.OrderBy(x => x.Title),
                "starttime" => searchModel.OrderBy == "DESC" ?
                    query.OrderByDescending(x => x.StartOn) :
                    query.OrderBy(x => x.Title),
                "endtime" => searchModel.OrderBy == "DESC" ?
                    query.OrderByDescending(x => x.StartOn) :
                    query.OrderBy(x => x.Title),
                _ => query
            };


            var result = query
                .Select(x =>
                    new FAQ
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Conten = x.Conten,
                        StarTime = x.StartOn,
                        EndTime = x.EndOn,
                        Sort = x.Sort,
                        UpperId = x.UpperId
                    });

            return new GetFAQAll
            {
                AllFAQs = result
            };
        }

        /// <summary>
        /// 取得頁數資料
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paged"></param>
        /// <returns></returns>
        public async Task<PageList> GetPageList<T>(IQueryable<T> query, Paged paged)
        {
            var totalCount = await query.CountAsync();

            var totalPage = totalCount % paged.PageSize == 0
                ? totalCount / paged.PageSize
                : totalCount / paged.PageSize + 1;

            var result = new PageList()
            {
                NowPage = paged.NowPage,
                TotalCount = totalCount,
                TotalPage = totalPage,
                PageSize = paged.PageSize
            };
            return result;
        }

        /// <summary>
        /// 分頁
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paged"></param>
        /// <returns></returns>
        public IQueryable<T> GetSkipTakeData<T>(IQueryable<T> query, Paged paged)
        {
            return query.Skip((paged.NowPage - 1) * paged.PageSize).Take(paged.PageSize);
        }

        /// <summary>
        /// 取得路徑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFAQUpperName(int? id)
        {
            var name = "";

            if (id == null)
                return name;

            var query = _context.QaData.FirstOrDefault(x => x.Id == id);

            if (query.UpperId != null)
            {
                name = GetFAQUpperName(query.UpperId.Value);
            }

            name += query.Title + ">";

            return name;
        }

        /// <summary>
        /// 取得單筆詳細資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<FAQ> GetByID(int id)
        {
            var query = await _context.QaData.FirstOrDefaultAsync(x => x.Id == id);


            var result = new FAQ
            {
                Id = query.Id,
                Conten = query.Conten,
                Title = query.Title,
                Sort = query.Sort,
                StarTime = query.StartOn,
                EndTime = query.EndOn,
                UpperId = query.UpperId
            };

            return result;
        }

        /// <summary>
        /// 刪除FAQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(int id)
        {
            var query = _context.QaData.FirstOrDefault(x => x.Id == id);
            query.Remove = true;
            query.UpdateBy = "ME";
            query.UpdateTime = DateTime.Now;
            _context.Update(query);
            var delete = await _context.SaveChangesAsync();
            if (delete >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// 更新FAQ
        /// </summary>
        /// <param name="updateFAQ"></param>
        /// <returns></returns>
        public async Task<bool> UpdateFAQ(FAQ updateFAQ)
        {
            var query = _context.QaData.FirstOrDefault(x => x.Id == updateFAQ.Id);
            query.Title = updateFAQ.Title;
            query.Conten = updateFAQ.Conten;
            query.Sort = updateFAQ.Sort;
            query.UpperId = updateFAQ.UpperId;
            query.EndOn = updateFAQ.EndTime;
            query.StartOn = updateFAQ.StarTime;
            query.UpdateBy = "me";
            query.UpdateTime = DateTime.Now;

            _context.Update(query);
            var update = await _context.SaveChangesAsync();
            if (update >= 0)
                return true;
            return false;
        }

        /// <summary>
        /// 搜尋搜尋FAQ(單一層級)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<SearchById> SearchById(int? id, string keyword)
        {
            keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;

            if (id == null)
            {
                return new SearchById { SubSearch = SearchSunById(id, keyword) };
            }

            var query =
                 _context.QaData.FirstOrDefault(
                 x =>
                 x.Remove == false &&
                 x.StartOn <= DateTime.Now &&
                 (x.EndOn == null || x.EndOn >= DateTime.Now) &&
                 x.Id == id);

            if (query == null)
                return new SearchById();

            var result = new SearchById
            {
                Title = query.Title,
                Id = query.Id,
                Conten = query.Conten,
                UpperName = GetFAQUpperName(query.UpperId),
                SubSearch = SearchSunById(query.Id, keyword)
            };

            return result;
        }

        /// <summary>
        /// 搜尋FAQ指定ID子目標
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyword"></param>
        /// <returns></returns>
        private IEnumerable<SubFAQ> SearchSunById(int? id, string keyword)
        {
            var result = _context.QaData.Where(
                x =>
                x.Remove == false &&
                x.StartOn <= DateTime.Now &&
                (x.EndOn == null || x.EndOn >= DateTime.Now) &&
                x.UpperId == id &&
                (x.Title.Contains(keyword) || x.Conten.Contains(keyword))
                )
                .OrderBy(x => x.Sort)
                .Select(x =>
                                new SubFAQ
                                {
                                    Id = x.Id,
                                    UpperId = x.UpperId,
                                    Title = x.Title,
                                    Conten = x.Conten,
                                    Sort = x.Sort
                                });
            return result;
        }

        /// <summary>
        /// 巢狀(多次資料表)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<LevelList> GetLevelList(int? id)
        {
            var query = _context.QaData.Where(x => x.UpperId == id).ToList().Select(x => new LevelList
            {
                Id = x.Id,
                Titel = x.Title,
                Sort = x.Sort,
                UpperId = x.UpperId,
                Conten = x.Conten,
                SubLevelList = GetLevelList(x.Id),
            });


            return query;
        }

        /// <summary>
        /// 取得上層所有路徑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetFAQAllUpperId(int? id)
        {
            if (id == null)
            {
                return "";
            }
            var query = _context.QaData.FirstOrDefault(x => x.Id == id.Value)?.UpperAllid;

            if (string.IsNullOrEmpty(query))
                query += ",";
            query += id + ",";

            return query;
        }

        /// <summary>
        /// 巢狀(一次取大量)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<LevelList> GetLevelListAll(int? id)
        {
            var query = _context.QaData.Where(x => !x.Remove).AsEnumerable();

            var result = query.Where(x => x.UpperId == id).OrderBy(x => x.Sort).Select(x => new LevelList
            {
                UpperId = x.UpperId,
                Id = x.Id,
                Sort = x.Sort,
                Titel = x.Title,
                Conten = x.Conten,
                SubLevelList = GetAllListById(x.Id, query)
            });


            return result;
        }

        /// <summary>
        /// 取得子問題
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        private IEnumerable<LevelList> GetAllListById(int? id, IEnumerable<QaData> query)
        {
            if (!query.Any(x => x.UpperId == id))
                return null;
            var result = query.Where(x => x.UpperId == id).Select(x => new LevelList
            {
                UpperId = x.UpperId,
                Id = x.Id,
                Sort = x.Sort,
                Conten = x.Conten,
                Titel = x.Title,
                SubLevelList = GetAllListById(x.Id, query)
            });

            return result;
        }

        /// <summary>
        /// 前台巢狀(一次大量) 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        public IEnumerable<LevelList> SearchAllById(int? id, string keyWord)
        {
            var query = _context.QaData.Where(x => !x.Remove && 
                                                   x.StartOn<=DateTime.Now &&
                                                   (x.EndOn == null ||
                                                    x.EndOn >= DateTime.Now)).AsEnumerable();

            var result = query.Where(x => x.UpperId == id).OrderBy(x => x.Sort).Select(x => new LevelList
            {
                UpperId = x.UpperId,
                Id = x.Id,
                Sort = x.Sort,
                Titel = x.Title,
                Conten = x.Conten,
                SubLevelList = GetAllListById(x.Id, query)
            });

            if (!string.IsNullOrEmpty(keyWord))
                result = result.Where(x => x.Titel.Contains(keyWord) || x.Conten.Contains(keyWord));

            return result;
        }

        public Task<bool> UpdateLevel(IEnumerable<UpdateLevel> updateLevels)
        {
            foreach (var updateLevel in updateLevels)
            {
                
            }

            throw new NotImplementedException();
        }
    }
}
