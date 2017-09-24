using MeuSite.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace TesteFiap
{
    public class HomeControlletTest
    {

        [Fact]
        public async Task dado_uma_lista_de_receitas_do_repository_elas_devem_ser_retornadas_em_ordem_alfabetica()
        {
            var repository = new Moq.Mock<IRepository>();
            repository.Setup(a => a.List(It.IsAny<int>())).Returns(new List<Recipe>());
            repository.Setup(a => a.List(2)).Returns(new List<Recipe>());
            repository.Setup(a => a.List(3)).Returns(new List<Recipe>());

        }

        [Fact]
        public async Task dado_um_repository_sem_receitas_deve_retornar_uma_view_chamada_norecipes()
        {
            var respository = new Moq.Mock<IRepository>();
            respository.Setup(a => a.List(It.IsAny<int>())).Returns(new List<Recipe>());

            var controller = new HomeController(respository.Object);
            var result = await controller.Index();

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal("NoRecipes", viewResult.ViewName);

        }
    }
}
