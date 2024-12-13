using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLC
{
    public interface IResultadosService
    {
        // Crear Resultado
        ResultadosEvaluaciones CreateResultados(ResultadosEvaluaciones resultadosevaluaciones);

        // Eliminar un Resultado por ID
        bool Delete(int id);

        // Obtener todos los Resultados
        List<ResultadosEvaluaciones> GetAll();

        // Obtener un Resultado por ID
        ResultadosEvaluaciones GetById(int id);

        // Actualizar un Resultado
        bool UpdateProduct(ResultadosEvaluaciones resultadosevaluaciones);
    }
}