using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2019
{
    class Day14
    {
        public void Task1()
        {
            IDictionary<string, IList<Materials>> materials = ReadAndParse();
            int minORE = int.MaxValue;
            FindMaterials(materials, materials["FUEL"][0].inputQuantities, ref minORE);

            long result = 0;


            //2842648
            Console.WriteLine("Day 14 task 1 : " + result);
        }

        public void Task2()
        {
            IDictionary<string, IList<Materials>> materials = ReadAndParse();


            long result = 0;


            //2842648
            Console.WriteLine("Day 14 task 2 : " + result);
        }

        private void FindMaterials(IDictionary<string, IList<Materials>> materials, IDictionary<string, int> needMaterials, ref int minORE)
        {
            if(needMaterials.Count == 1 && needMaterials.ContainsKey("ORE"))
            {
                if(needMaterials["ORE"] < minORE) { 
                    minORE = needMaterials["ORE"]; }
                //Console.WriteLine(needMaterials["ORE"]);
                return;
            }

            if(needMaterials.ContainsKey("ORE") && needMaterials["ORE"] >= minORE)
            {
                return;
            }

            foreach (string key in needMaterials.Keys)
            {
                if ("ORE" == key) { continue; }

                IDictionary<string, int> needMaterialsNew = new Dictionary<string, int>();
                foreach (string keyNew in needMaterials.Keys)
                {
                    if (keyNew != key) { needMaterialsNew[keyNew] = needMaterials[keyNew]; }
                }

                int quantity = materials[key][0].outputQuantity;
                int factor = (int)Math.Ceiling((double)needMaterials[key] / (double)quantity);
                foreach (Materials material in materials[key])
                {
                    foreach (string inputMaterial in material.inputQuantities.Keys)
                    {
                        if (!needMaterialsNew.ContainsKey(inputMaterial)) { needMaterialsNew[inputMaterial] = 0; }
                        needMaterialsNew[inputMaterial] += factor * material.inputQuantities[inputMaterial];
                    }
                }
                if (needMaterialsNew.ContainsKey("ORE") && needMaterialsNew["ORE"] >= minORE)
                {
                    continue;
                }
                FindMaterials(materials, needMaterialsNew, ref minORE);
            }
        }

        private IDictionary<string, IList<Materials>> ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day14.txt");
            //AdventUtils.WriteLines(lines);

            IDictionary<string, IList<Materials>> materials = new Dictionary<string, IList<Materials>>();
            foreach(string line in lines)
            {
                string[] input = line.Split(new string[2] { ",", "=>"}, StringSplitOptions.None);
                string[] endMaterial = input[input.Length - 1].Trim().Split(new string[1] { " " }, StringSplitOptions.None);
                int endMaterialQuantity = int.Parse(endMaterial[0]);
                string endMaterialName = endMaterial[1];
                if (!materials.ContainsKey(endMaterialName)) { materials[endMaterialName] = new List<Materials>(); }
                Materials materialsParts = new Materials { outputQuantity = endMaterialQuantity, inputQuantities = new Dictionary<string,int>() };
                materials[endMaterialName].Add(materialsParts);

                string[] partMaterial = line.Substring(0,line.IndexOf(" =>")).Split(new string[1] { "," }, StringSplitOptions.None);
                for (int index = 0; index < partMaterial.Length; index = index + 1)
                {
                    int quantity = int.Parse(partMaterial[index].Trim().Split(' ')[0].Trim());
                    string material = partMaterial[index].Trim().Split(' ')[1].Trim();
                    materialsParts.inputQuantities[material] = quantity;
                }
            }

            return materials;
        }
    }

    class Materials
    {
        public int outputQuantity { get; set; }
        public IDictionary<string, int> inputQuantities { get; set; }
    }
}
