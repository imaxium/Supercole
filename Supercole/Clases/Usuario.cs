using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supercole.Clases
{
    class Usuario
    {
        private string id_codigo;
        private string usuario_nombre;
        private string usuario_nickname;
        private string id_tipo_usuario;

        public Usuario(string id_codigo, string usuario_nombre, string usuario_nickname, string id_tipo_usuario)
        {
            this.id_codigo = id_codigo;
            this.usuario_nombre = usuario_nombre;
            this.usuario_nickname = usuario_nickname;
            this.id_tipo_usuario = id_tipo_usuario;
        }

        public string Id_codigo { get => id_codigo; set => id_codigo = value; }
        public string Usuario_nombre { get => usuario_nombre; set => usuario_nombre = value; }
        public string Usuario_nickname { get => usuario_nickname; set => usuario_nickname = value; }
        public string Id_tipo_usuario { get => id_tipo_usuario; set => id_tipo_usuario = value; }

    }
}
