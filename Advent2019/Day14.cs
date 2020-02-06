using System;
using System.Collections.Generic;
using System.Text;

namespace Advent2019
{
    class Day14
    {
        public void Task1()
        {
            IDictionary<Material, IList<Material>> materials = ReadAndParse();

            IList<Material> needMaterials = new List<Material>() { new Material() { Name = "FUEL", Quantity = 1 } };
            IList<Material> endList = FindMaterials(materials, needMaterials);
            long result = CalculateOre(endList);

            //278404
            Console.WriteLine("Day 14 task 1 : " + result);
        }

        public void Task2()
        {
            IDictionary<Material, IList<Material>> materials = ReadAndParse();
            long result = 0;
            for (long x = 4436900; x < 4500000; x++)
            {
                IList<Material> needMaterials = new List<Material>() { new Material() { Name = "FUEL", Quantity = x } };
                IList<Material> endList = FindMaterials(materials, needMaterials);
                long numOfOre = CalculateOre(endList);

                if(numOfOre > 1000000000000)
                {
                    result = --x; break;
                }
            }

            //4436981
            Console.WriteLine("Day 14 task 1 : " + result);
        }

        private long CalculateOre(IList<Material> endList)
        {
            long result = 0;
            foreach (Material material in endList)
            {
                result += material.Quantity;
            }

            return result;
        }

        private IList<Material> FindMaterials(IDictionary<Material, IList<Material>> materials, IList<Material> needMaterials)
        {
            IList<Material> wastedMaterials = new List<Material>();
            for (int index = 0; index < needMaterials.Count; index++)
            {
                if (!materials.ContainsKey(needMaterials[index]) )
                {
                    continue;
                }

                long quantityNeeded = needMaterials[index].Quantity;
                long quantityProvided = 0;
                foreach (Material materialWasted in wastedMaterials)
                {
                    if (materialWasted.Quantity > 0 && needMaterials[index].Name == materialWasted.Name)
                    {
                        if (quantityNeeded >= materialWasted.Quantity)
                        {
                            quantityNeeded -= materialWasted.Quantity;
                            materialWasted.Quantity = 0;
                        }
                        else
                        {
                            materialWasted.Quantity -= quantityNeeded;
                            quantityNeeded = 0;
                        }
                    }
                }
                if (quantityNeeded == 0)
                {
                    needMaterials.RemoveAt(index);
                    index = -1;
                    continue;
                }

                foreach (Material materialBase in materials.Keys)
                {
                    if (materialBase.Equals(needMaterials[index]))
                    {
                        quantityProvided = materialBase.Quantity; break;
                    }
                }
                long factor = (long)Math.Ceiling((double)quantityNeeded / (double)quantityProvided);
                long wasted = (factor * quantityProvided) - quantityNeeded;
                if (wasted > 0)
                    wastedMaterials.Add(new Material() { Name = needMaterials[index].Name, Quantity = wasted });

                foreach (Material material in materials[needMaterials[index]])
                {
                    needMaterials.Add(new Material() { Name = material.Name, Quantity = factor * material.Quantity });
                }

                needMaterials.RemoveAt(index);
                index = -1;
            }
            
            return needMaterials;
        }

        private IDictionary<Material, IList<Material>> ReadAndParse()
        {
            IList<string> lines = AdventUtils.ReadFileByLines(@"..\..\..\Files\Day14.txt");
            //AdventUtils.WriteLines(lines);

            IDictionary<Material, IList<Material>> materials = new Dictionary<Material, IList<Material>>();
            foreach (string line in lines)
            {
                string[] input = line.Split(new string[2] { ",", "=>" }, StringSplitOptions.None);
                string[] endMaterial = input[input.Length - 1].Trim().Split(new string[1] { " " }, StringSplitOptions.None);
                int endMaterialQuantity = int.Parse(endMaterial[0]);
                string endMaterialName = endMaterial[1];
                Material material = new Material() { Name = endMaterialName, Quantity = endMaterialQuantity };
                if (!materials.ContainsKey(material)) { materials[material] = new List<Material> (); }
                
                string[] partMaterial = line.Substring(0, line.IndexOf(" =>")).Split(new string[1] { "," }, StringSplitOptions.None);
                for (int index = 0; index < partMaterial.Length; index = index + 1)
                {
                    int quantity = int.Parse(partMaterial[index].Trim().Split(' ')[0].Trim());
                    string materialName = partMaterial[index].Trim().Split(' ')[1].Trim();
                    materials[material].Add(new Material() { Name = materialName, Quantity = quantity });
                }
            }

            return materials;
        }
    }

    class Material
    {
        public string Name { get; set; }
        public long Quantity { get; set; }

        public override bool Equals(object obj)
        {
            Material item = obj as Material;

            if (item.Name == Name)
            {
                return true;
            }

            return false;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }
}