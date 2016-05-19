using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace GeradorChaves
{
    public class Chaves
    {
        /// <summary>
        /// 
        /// </summary>
        private static char[] Chars => "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        
        /// <summary>
        /// Metodo teste para verificar colisões entre chaves conforme (N) posições e (N? = 15)
        /// </summary>
        /// <param name="numCaracteres"></param>
        /// <param name="tempoMinutos"></param>
        /// <returns>Retorna objeto Colisao</returns>
        public Colisao VerficaColisoesRngCrypto(int numCaracteres, int? tempoMinutos = 15)
        {
            var hs = new HashSet<string>();
            int contador = 0;
            var date = DateTime.Now;
            bool ocorrencia = false;

            // Create new stopwatch.
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            do
            {
                contador++;

                Thread.Sleep(2);

                var chave = ObtemChaveExclusiva(numCaracteres);

                hs.Add(chave);

                Debug.Print("chave:{0} - contador:{1}", chave, contador);

                if (hs.Count(x => x == chave) < 2) continue;

                Debug.Print("| -- colidiu -- |");
                ocorrencia = true;

                break;

            } while ((DateTime.Now - date).Minutes <= tempoMinutos);

            stopwatch.Stop();

            return new Colisao
            {
                TempoMinutos = stopwatch.Elapsed.Minutes,
                TotalTentativas = contador,
                Ocorrencia = ocorrencia
            };
        }

        /// <summary>
        /// Gera chave randomica exclusiva
        /// </summary>
        /// <param name="maxSize"></param>
        /// <returns></returns>
        public string ObtemChaveExclusiva(int maxSize)
        {
            var crypto = new RNGCryptoServiceProvider();

            byte[] data = new byte[1];

            crypto.GetNonZeroBytes(data);

            data = new byte[maxSize];

            crypto.GetNonZeroBytes(data);

            var result = new StringBuilder(maxSize);

            foreach (byte b in data) result.Append(Chars[b % (Chars.Length)]);

            return result.ToString();
        }
    }
}
