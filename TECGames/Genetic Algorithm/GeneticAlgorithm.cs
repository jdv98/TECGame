using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

namespace TECGames.Genetic_Algorithm
{

    class GeneticAlgorithm
    {
        Dictionary<int, int> parents = new Dictionary<int, int>();

        List<Work> works = new List<Work>();

        List<Work> actualGeneration = new List<Work>();
        
        public GeneticAlgorithm(List<Work> works)
        {
            this.works = works;
        }

        public void Courtship(List<Work> works) {

            works.Sort((a, b) => a.WorkSection.Price.CompareTo(b.WorkSection.Price));

            foreach (Work w in works)
            {
                for (int i = works.Count; i > 0; i--) {

                    if (w.Id != works[i].Id && w.WorkSection.Price > works[i].WorkSection.Price) {

                        parents.Add(w.Id, works[i].Id);
                    }
                }
            }
        }

        public void Crossing()
        {
            foreach (KeyValuePair<int, int> w in parents)
            {
                //REPRODUCIR CREAR NUEVAS INSTANCIAS DE TRABAJO, ID? , HACER VERIFICACIONES DE HORARIOS.
            }
        }

        

    }
}
