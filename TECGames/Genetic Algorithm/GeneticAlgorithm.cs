using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TECGames.Diagram_classes;

namespace TECGames.Genetic_Algorithm
{
    class GeneticAlgorithm
    {
        List<Work> works = new List<Work>();

        List<Work> actualGeneration = new List<Work>();

        public GeneticAlgorithm(List<Work> works)
        {
            this.works = works;
        }

        public void Courtship() {

            foreach(Work w in works)
            {
                for (int i = works.Count; i > 0; i--) {
                    

                }
            }


        }


    }
}
