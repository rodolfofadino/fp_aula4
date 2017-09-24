using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace TesteFiap
{
    public class IngredientesSplitterTest
    {
        [Fact]
        public void dado_uma_texto_vazio_de_ingredientes_retornar_uma_lista_vazia()
        {
            var textoIngredientes = string.Empty;

            Splitter splitter = new Splitter();
            var result = splitter.Split(textoIngredientes);

            Assert.IsType<List<string>>(result);
            Assert.Empty(result);
        }

        [Fact]
        public void dado_um_texto_com_dois_ingradientes_separado_por_virgula_retorna_uma_lista_com_eles()
        {
            var texto = "farinha,açucar";

            var splitter = new Splitter();
            var result = splitter.Split(texto);

            Assert.Equal(2, result.Count);
            Assert.Equal("farinha", result[0]);
            Assert.Equal("açucar", result[1]);
        }
        [Fact]
        public void dado_um_separador_e_um_texto_com_tres_ingredientes_retornar_uma_lista_com_os_ingredientes()
        {
            const int totalExperadoDeItensDeIngredientes = 3;
            var separador = ';';
            var texto = "arroz;batata;feijao";
            var splitter = new Splitter(separador);
            var result = splitter.Split(texto);

            Assert.Equal(totalExperadoDeItensDeIngredientes, result.Count);
        }

        [Fact]
        public void dado_um_separator_service_com_ponto_e_virgula_e_um_texto_com_tres_ingredientes_retornar_os_ingredientes()
        {
            const int totalDeAlimentosEsperado = 3;
            var mockService = new Moq.Mock<ISeparatorService>();
            mockService.Setup(a => a.GetSeparator()).Returns(';');

            var texto = "arroz;feijao;batata";

            var splitter = new Splitter(mockService.Object);
            var result = splitter.Split(texto);

            Assert.Equal(totalDeAlimentosEsperado, result.Count);

            mockService.Verify(a => a.GetSeparator(), Moq.Times.Once);
        }
    }

    public class Splitter
    {
        private char _separador;
        private ISeparatorService _service;

        public Splitter(char separador = ',')
        {
            _separador = separador;
        }

        public Splitter(ISeparatorService service)
        {
            this._service = service;
        }

        internal List<string> Split(string textoIngredientes)
        {
            if (string.IsNullOrWhiteSpace(textoIngredientes))
                return new List<string>();

            //return textoIngredientes.Split(_service.GetSeparator()).ToList();
            return textoIngredientes.Split(';').ToList();
        }
    }

    public class SeparatorService : ISeparatorService
    {
        public char GetSeparator()
        {
            return ',';
        }
    }
    public interface ISeparatorService
    {
        char GetSeparator();
    }
}
