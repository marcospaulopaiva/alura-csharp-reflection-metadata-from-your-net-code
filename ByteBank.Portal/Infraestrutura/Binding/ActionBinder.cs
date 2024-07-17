using System;
using System.Collections.Generic;

namespace ByteBank.Portal.Infraestrutura.Binding
{
    internal class ActionBinder
    {
        public object ObterMethodInfo(object controller, string path)
        {
            var idxInterrogacao = path.IndexOf('?');
            var existeQueryString = idxInterrogacao >= 0;

            if (!existeQueryString) 
            {
                var nomeAction = path.Split(new char[] {'/'}, StringSplitOptions.RemoveEmptyEntries)[1];
                var methodInfo = controller.GetType().GetMethod(nomeAction);
                return methodInfo;
            }
            else
            {
                var nomeControllerComAction = path.Substring(0,idxInterrogacao);
                var nomeAction = nomeControllerComAction.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries)[1];
                var queryString = path.Substring(idxInterrogacao+1);

                var tuplasNomeValor = ObterArgumentoNomeValor(queryString);
            }
        }

        private IEnumerable<ArgumentoNomeValor> ObterArgumentoNomeValor(string queryString)
        {
            var tuplasNomeValor = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var tupla in tuplasNomeValor)
            {
                var partesTupla = tupla.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                yield return new ArgumentoNomeValor(partesTupla[0], partesTupla[1]);
            }
        }
    }
}
