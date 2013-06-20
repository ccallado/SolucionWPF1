using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProyectoWPF1
{
    class Clase1
    {
        int Dato1;
        public int Dato2;
        int _Dato3;

        public int Dato3
        {
            get { return _Dato3; }
            set { _Dato3 = value; }
        }
        //Permito sobreescribir la propiedad
        public virtual int Dato4 { get; set; }

        #region Procedimientos
        public void M1()
        { }

        //Permito sobreescribir el método
        public virtual void M1(int x)
        { }

        public void M1(string x)
        { }
        #endregion
        #region Constructores
        protected Clase1()
        { }

        public Clase1(int Dato1)
        {
            this.Dato1 = Dato1;
        }

        public Clase1(int Dato1, int Dato2)
            : this(Dato1)
        {
            this.Dato2 = Dato2;
        }

        #endregion
    }
}

namespace ProyectoWPF1bis
{
    class Clase1
    {

    }

    class Clase2 : ProyectoWPF1.Clase1
    {
        public Clase2() { }
        public Clase2(int Dato1, int Dato2)
            : base(Dato1, Dato2)
        {
        }

        public override int Dato4
        {
            get
            {
                return base.Dato4;
            }
            set
            {
                base.Dato4 = value;
            }
        }

        public override void M1(int x)
        {
            base.M1(x);
        }

    }
}

namespace System
{
    class Clase1deJavier
    {

    }
}

