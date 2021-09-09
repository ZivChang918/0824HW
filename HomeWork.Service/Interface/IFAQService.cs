using HomeWork.DTO.Model.ApiDTO;
using HomeWork.DTO.Model.ServiceDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork.Service.Interface
{
    public interface IFAQService
    {
        /// <summary>
        /// 新增FAQ
        /// </summary>
        /// <param name="addFAQ"></param>
        /// <returns></returns>
        Task<bool> AddFAQ(AddFAQ addFAQ);
        /// <summary>
        /// 更新FAQ
        /// </summary>
        /// <param name="updateFAQ"></param>
        /// <returns></returns>
        Task<bool> UpdateFAQ(FAQ updateFAQ);
        /// <summary>
        /// 刪除FAQ
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(int id);
        /// <summary>
        /// 關鍵字搜尋FAQ--後台
        /// </summary>
        /// <param name="searchModel"></param>
        /// <returns></returns>
        GetFAQAll GetByKeyWord(SearchModel searchModel);

        /// <summary>
        /// 取得巢狀(多次讀取)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<LevelList> GetLevelList(int? id);

        /// <summary>
        /// 取得巢狀(全讀取)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<LevelList> GetLevelListAll(int? id);

        /// <summary>
        /// 取得單筆詳細
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<FAQ> GetByID(int id);
        /// <summary>
        /// 取得分頁資訊
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paged"></param>
        /// <returns></returns>
        Task<PageList> GetPageList<T>(IQueryable<T> query, Paged paged);
        /// <summary>
        /// 區分分頁內容
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="paged"></param>
        /// <returns></returns>
        IQueryable<T> GetSkipTakeData<T>(IQueryable<T> query, Paged paged);
        /// <summary>
        /// 取得上層名稱
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetFAQUpperName(int? id);

        /// <summary>
        /// 取得所有上層ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        string GetFAQAllUpperId(int? id);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="updateLevels">母子順序</param>
        /// <returns></returns>
        Task<bool> UpdateLevel(IEnumerable<UpdateLevel> updateLevels);

        /// <summary>
        /// 搜尋FAQ--前台
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        Task<SearchById> SearchById(int? id, string keyWord);

        /// <summary>
        /// 搜尋FAQ--前台(全部)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="keyWord"></param>
        /// <returns></returns>
        IEnumerable<LevelList> SearchAllById(int? id, string keyWord);
    }
}
