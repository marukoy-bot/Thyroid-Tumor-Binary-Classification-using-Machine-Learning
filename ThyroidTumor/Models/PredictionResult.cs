using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThyroidTumor.Models
{
    public class PredictionResult
    {
        public string label {  get; set; }
        public double prediction_value { get; set; }
    }
}
