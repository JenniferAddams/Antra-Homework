using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieStoreMVC.Views.Shared.Components.Genres
{
    public class GenresViewComponent : ViewComponent
    //go to services layer and get geners data, 使用方法类似 controller，命名也类似。
    {
        private readonly IGenreService _genreService;
        //Ctrl.   会可以自己生成constructor
        public GenresViewComponent(IGenreService genreService)
        {
            _genreService = genreService;
        }
        public async Task<IViewComponentResult> InvokeAsync()//get all geners data from IGENRESERVICE 
        {
            var genres = await _genreService.GetAllGenres();
            return View(genres);//return default.cshtml
        }
    }
}
