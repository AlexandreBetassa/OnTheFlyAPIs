using System;

namespace Models
{
    public class Utils
    {
        public static bool ValidateCnpj(string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma, resto;
            string digito, tempCnpj;

            //limpa caracteres especiais e deixa em minusculo
            cnpj = cnpj.ToLower().Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "").Replace(" ", "");
            cnpj = cnpj.Replace("+", "").Replace("*", "").Replace(",", "").Replace("?", "");
            cnpj = cnpj.Replace("!", "").Replace("@", "").Replace("#", "").Replace("$", "");
            cnpj = cnpj.Replace("%", "").Replace("¨", "").Replace("&", "").Replace("(", "");
            cnpj = cnpj.Replace("=", "").Replace("[", "").Replace("]", "").Replace(")", "");
            cnpj = cnpj.Replace("{", "").Replace("}", "").Replace(":", "").Replace(";", "");
            cnpj = cnpj.Replace("<", "").Replace(">", "").Replace("ç", "").Replace("Ç", "");

            // Se vazio
            if (cnpj.Length == 0) return false;
            //Se o tamanho for < 14 então retorna como falso
            if (cnpj.Length != 14) return false;
            // Caso coloque todos os numeros iguais
            switch (cnpj)
            {

                case "00000000000000": return false;
                case "11111111111111": return false;
                case "22222222222222": return false;
                case "33333333333333": return false;
                case "44444444444444": return false;
                case "55555555555555": return false;
                case "66666666666666": return false;
                case "77777777777777": return false;
                case "88888888888888": return false;
                case "99999999999999": return false;
            }
            tempCnpj = cnpj.Substring(0, 12);
            //cnpj é gerado a partir de uma função matemática, logo para validar, sempre irá utilizar esse calculo 
            soma = 0;
            for (int i = 0; i < 12; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;
            for (int i = 0; i < 13; i++) soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2) resto = 0;
            else resto = 11 - resto;
            digito += resto.ToString();
            return cnpj.EndsWith(digito);
        }

        public static bool CPFIsValid(string unformattedCpf)
        {
            string cpfString = unformattedCpf;
            int digVerificador, v1, v2, aux;
            int[] digitosCPF = new int[9];

            if (unformattedCpf.Length != 11) return false;

            if (!long.TryParse(cpfString, out long cpfLong)) return false;
            
            digVerificador = (int)(cpfLong % 100);
            cpfLong /= 100;
            for (int i = 0; i < 9; i++)
            {
                aux = (int)(cpfLong % 10);
                digitosCPF[i] = aux;
                cpfLong /= 10;
            }
            for (int i = 0; i < digitosCPF.Length; i++)
            {
                if (i == digitosCPF.Length - 1) return false;
                if (digitosCPF[i] != digitosCPF[i + 1]) break;
            }
            v1 = v2 = 0;
            for (int i = 0; i < 9; i++)
            {
                v1 += digitosCPF[i] * (9 - i);
                v2 += digitosCPF[i] * (8 - i);
            }
            v1 = (v1 % 11) % 10;
            v2 += v1 * 9;
            v2 = (v2 % 11) % 10;
            if (v1 * 10 + v2 == digVerificador) return true;
            else return false;
        }

        public static string FormatCNPJ(string unformatedCNPJ) => $"{unformatedCNPJ.Substring(0, 2)}." +
           $"{unformatedCNPJ.Substring(2, 3)}." +
           $"{unformatedCNPJ.Substring(5, 3)}/" +
           $"{unformatedCNPJ.Substring(8, 4)}-" +
           $"{unformatedCNPJ.Substring(12, 2)}";
      
        public static bool ValidateRab(string rab)
        {
            string prefix = rab.Substring(0, 2);
            string sufix = rab.Substring(3, 3);
            if (prefix != "PP" && prefix != "PR" && prefix != "PT" && prefix != "BR" && prefix != "PS") return false;
            else if (sufix == "SOS" && sufix == "XXX" && sufix == "PAN" && sufix == "TTT" && sufix == "VFR"
                && sufix == "IFR" && sufix == "VMC" && sufix == "IMC") return false;
            else return true;
        }

        public static string FormatCPF(string unformatedCpf)
            => $"{unformatedCpf.Substring(0, 3)}." +
            $"{unformatedCpf.Substring(3, 3)}." +
            $"{unformatedCpf.Substring(6, 3)}-" +
            $"{unformatedCpf.Substring(9, 2)}";

        public static bool ValidateCompanyTime(Company company)
        {
            double time = (DateTime.Now - company.DtOpen).TotalDays;
            if (time / 30 < 6) return false;
            else return true;
        }
    }
}
