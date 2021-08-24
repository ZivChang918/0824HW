using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Windows.Data;
using HomeWork.Service.Interface;
using HomeWork.DTO.Model.ServiceDTO;
using HomeWork.DTO.Model.ApiDTO;

namespace HomeWork.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FAQController : ControllerBase
    {
        private readonly IFAQService _faqService;
        public FAQController(IFAQService faqService)
        {
            _faqService = faqService;
        }

        /// <summary>
        /// 後台-搜尋FAQ
        /// </summary>
        /// <returns></returns>
        [HttpGet("KeyWord")]
        public async Task<IActionResult> GetByKeyWord([FromQuery] SearchModel searchModel)
        {
            var query = _faqService.GetByKeyWord(searchModel);
            var pagedList = await _faqService.GetPageList(query.AllFAQs, searchModel);
            query.AllFAQs = _faqService.GetSkipTakeData(query.AllFAQs, searchModel);
            query.AllFAQs = query.AllFAQs.ToList().Select(x => new FAQ
            {
                Title = x.Title,
                Conten = x.Conten,
                StarTime = x.StarTime,
                EndTime = x.EndTime,
                id = x.id,
                Sort = x.Sort,
                UpperId = x.UpperId,
                UpperName = _faqService.GetFAQUpperName(x.UpperId)
            }).AsQueryable();

            var result = new PagedData<GetFAQAll>
            {
                Datas = query,
                Paged = pagedList
            };
            return Ok(result);
        }


        /// <summary>
        /// 取得單筆FAQ明細
        /// </summary>
        /// <returns></returns>
        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetFAQDetail(int id)
        {
            var query = _faqService.GetByID(id);
            var result = new FAQDetail()
            {
                id = query.id,
                Conten = query.Conten,
                Title = query.Title,
                Sort = query.Sort,
                StarTime = query.StarTime,
                EndTime = query.EndTime,
                UpperId = query.UpperId,
                UpperName = query.UpperName
            };

            return Ok(result);
        }


        /// <summary>
        /// 新增FAQ
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddFAQ(AddFAQApi addFaqApi)
        {
            try
            {
                var addFAQ = new AddFAQ
                {
                    StarTime = addFaqApi.StarTime,
                    UpperId = addFaqApi.UpperId,
                    Sort = addFaqApi.Sort,
                    Conten = addFaqApi.Conten,
                    EndTime = addFaqApi.EndTime,
                    Title = addFaqApi.Title
                };

                var addCheck = await _faqService.AddFAQ(addFAQ);

                if (addCheck)
                    return Ok("新增成功");
                return BadRequest("新增失敗");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 刪除FAQ
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFAQ(int id)
        {
            try
            {
                var DeleteCheck = await _faqService.Delete(id);

                if (DeleteCheck)
                    return Ok("刪除成功");
                return BadRequest("刪除失敗");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 更新FAQ詳細
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateFAQ([FromQuery] UpdateFAQApi updateFaq)
        {
            try
            {
                var updata = new FAQ
                {
                    id = updateFaq.id,
                    Title = updateFaq.Title,
                    Conten = updateFaq.Conten,
                    Sort = updateFaq.Sort,
                    StarTime = updateFaq.StarTime,
                    EndTime = updateFaq.EndTime,
                    UpperId = updateFaq.UpperId
                };
                var updateCheck = await _faqService.UpdateFAQ(updata);

                if (updateCheck)
                    return Ok("更新成功");
                return BadRequest("更新失敗");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// 後台-取得層級列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("level")]
        public async Task<IActionResult> GetLevelList()
        {
            var result = _faqService.GetLevelList(null);
            return Ok(result);
        }

        /// <summary>
        /// 前台-搜尋FAQ
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetByKeyWord(int? id,string keyword)
        {
            var result = _faqService.SearchById(id,keyword);
            return Ok(result);
        }

    }
}
