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
        public async Task<bool> AddFAQ(AddFAQ addFAQ)
        {
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
                UpperId = addFAQ.UpperId
            };
            await db.QaData.AddAsync(data);
            var add = await db.SaveChangesAsync();
            if (add >= 0)
                return true;
            return false;
        }


        public GetFAQAll GetByKeyWord(SearchModel searchModel)
        {
            var query = db.QaData.Where(x => x.Remove == false).OrderBy(x => x.UpperId).AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.KeyWord))
                query = db.QaData.Where(x =>
                    x.Conten.Contains(searchModel.KeyWord) || x.Title.Contains(searchModel.KeyWord));

            if (!(searchModel.StartOn == null))
                query = db.QaData.Where(x =>
                    x.StartOn >= searchModel.StartOn);


            if (!(searchModel.EndOn == null))
                query = db.QaData.Where(x =>
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
                        id = x.Id,
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

        public IQueryable<T> GetSkipTakeData<T>(IQueryable<T> query, Paged paged)
        {

            return query.Skip((paged.NowPage - 1) * paged.PageSize).Take(paged.PageSize);
        }

        public string GetFAQUpperName(int? id)
        {
            var name = "";

            if (id == null)
                return name;

            var query = db.QaData.FirstOrDefault(x => x.Id == id);

            if (query.UpperId != null)
            {
                name = GetFAQUpperName(query.UpperId.Value);
            }

            name += query.Title + ">";

            return name;
        }

        public FAQ GetByID(int id)
        {
            var query = db.QaData.FirstOrDefault(x => x.Id == id);


            var result = new FAQ
            {
                id = query.Id,
                Conten = query.Conten,
                Title = query.Title,
                Sort = query.Sort,
                StarTime = query.StartOn,
                EndTime = query.EndOn,
                UpperId = query.UpperId,
                UpperName = GetFAQUpperName(query.UpperId)
            };

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var query = db.QaData.FirstOrDefault(x => x.Id == id);
            query.Remove = true;
            query.UpdateBy = "ME";
            query.UpdateTime = DateTime.Now;
            db.Update(query);
            var delete = await db.SaveChangesAsync();
            if (delete >= 0)
                return true;
            return false;
        }

        public async Task<bool> UpdateFAQ(FAQ updateFAQ)
        {
            var query = db.QaData.FirstOrDefault(x => x.Id == updateFAQ.id);
            query.Title = updateFAQ.Title;
            query.Conten = updateFAQ.Conten;
            query.Sort = updateFAQ.Sort;
            query.UpperId = updateFAQ.UpperId;
            query.EndOn = updateFAQ.EndTime;
            query.StartOn = updateFAQ.StarTime;
            query.UpdateBy = "me";
            query.UpdateTime = DateTime.Now;

            db.Update(query);
            var update = await db.SaveChangesAsync();
            if (update >= 0)
                return true;
            return false;
        }

        public SearchById SearchById(int? id, string keyword)
        {
            keyword = string.IsNullOrEmpty(keyword) ? "" : keyword;

            if (id == null)
            {
                return new SearchById { SubSearch = SearchSunById(id, keyword) };
            }

            var query =
                db.QaData.FirstOrDefault(
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

        private IEnumerable<SubFAQ> SearchSunById(int? id, string keyword)
        {
            var result = db.QaData.Where(
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

        public IEnumerable<LevelList> GetLevelList(int? id)
        {
            var query = db.QaData.Where(x => x.UpperId == id).ToList().Select(x=>new LevelList
            {
                id = x.Id,
                Titel = x.Title,
                Sort = x.Sort,
                UpperId = x.UpperId,
                SubLevelList = GetLevelList(x.Id),
            });


            return query;
        }
    }
}
