using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Entities;

namespace BLL
{
    public class ResultadosLogic
    {
        public ResultadosEvaluaciones Create(ResultadosEvaluaciones resultado)
        {
            ResultadosEvaluaciones _resultado = null;

            using (var repository = RepositoryFactory.CreateRepository())
            {
                ResultadosEvaluaciones _existingResult = repository.Retrieve<ResultadosEvaluaciones>(r => r.EvaluacionID == resultado.EvaluacionID && r.EstudianteID == resultado.EstudianteID);

                if (_existingResult == null)
                {
                    _resultado = repository.Create(resultado);
                }
                else
                {
                    throw new Exception("El resultado de la evaluación ya existe para este estudiante.");
                }
            }

            return _resultado;
        }

        public ResultadosEvaluaciones RetrieveById(int id)
        {
            ResultadosEvaluaciones _resultado = null;
            using (var repository = RepositoryFactory.CreateRepository())
            {
                _resultado = repository.Retrieve<ResultadosEvaluaciones>(r => r.ResultadoID == id);
            }
            return _resultado;
        }

        public bool Update(ResultadosEvaluaciones resultado)
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Busca la entidad original en la base de datos
                var existingResult = repository.Retrieve<ResultadosEvaluaciones>(r => r.ResultadoID == resultado.ResultadoID);

                if (existingResult == null)
                {
                    throw new Exception("El resultado no existe.");
                }

                // Actualiza manualmente las propiedades necesarias
                existingResult.EvaluacionID = resultado.EvaluacionID;
                existingResult.EstudianteID = resultado.EstudianteID;
                existingResult.Calificacion = resultado.Calificacion;
                existingResult.FechaCalificacion = resultado.FechaCalificacion;

                // Guarda los cambios
                return repository.Update(existingResult);
            }
        }

        public bool Delete(int id)
        {
            bool _delete = false;
            var _resultado = RetrieveById(id);
            if (_resultado != null)
            {
                using (var repository = RepositoryFactory.CreateRepository())
                {
                    _delete = repository.Delete(_resultado);
                }
            }
            return _delete;
        }

        public List<ResultadosEvaluaciones> RetrieveAll()
        {
            using (var repository = RepositoryFactory.CreateRepository())
            {
                // Usar una expresión lambda para devolver una lista de ResultadosEvaluaciones
                var resultados = repository.Filter<ResultadosEvaluaciones>(r => r.ResultadoID > 0).ToList();
                return resultados;
            }
        }
    }
}