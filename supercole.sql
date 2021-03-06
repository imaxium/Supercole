PGDMP     :                    z            db_supercole    14.1    14.1                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false                       0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false                       0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16394    db_supercole    DATABASE     h   CREATE DATABASE db_supercole WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'Spanish_Spain.1252';
    DROP DATABASE db_supercole;
                postgres    false            ?            1259    16395    Alumnos    TABLE     ?   CREATE TABLE public."Alumnos" (
    id_alumno character(5) NOT NULL,
    alumno_nombre character varying(50),
    alumno_telefono integer,
    alumno_matricula integer,
    id_materia character(5),
    id_clase character(5)
);
    DROP TABLE public."Alumnos";
       public         heap    postgres    false            ?            1259    16398    Clases    TABLE     m   CREATE TABLE public."Clases" (
    id_clase character(5) NOT NULL,
    clase_nombre character varying(50)
);
    DROP TABLE public."Clases";
       public         heap    postgres    false            ?            1259    16401    Materias    TABLE     s   CREATE TABLE public."Materias" (
    id_materia character(5) NOT NULL,
    materia_nombre character varying(50)
);
    DROP TABLE public."Materias";
       public         heap    postgres    false            ?            1259    16404    TiposUsuarios    TABLE     z   CREATE TABLE public."TiposUsuarios" (
    id_tipo_usuario character(5) NOT NULL,
    tipo_nombre character varying(50)
);
 #   DROP TABLE public."TiposUsuarios";
       public         heap    postgres    false            ?            1259    16407    Usuarios    TABLE     ?   CREATE TABLE public."Usuarios" (
    id_codigo character(5) NOT NULL,
    usuario_nombre character varying(50),
    usuario_nickname character varying(10),
    usuario_contrasena character varying(10),
    id_tipo_usuario character(5)
);
    DROP TABLE public."Usuarios";
       public         heap    postgres    false                      0    16395    Alumnos 
   TABLE DATA           v   COPY public."Alumnos" (id_alumno, alumno_nombre, alumno_telefono, alumno_matricula, id_materia, id_clase) FROM stdin;
    public          postgres    false    209   ?                 0    16398    Clases 
   TABLE DATA           :   COPY public."Clases" (id_clase, clase_nombre) FROM stdin;
    public          postgres    false    210                    0    16401    Materias 
   TABLE DATA           @   COPY public."Materias" (id_materia, materia_nombre) FROM stdin;
    public          postgres    false    211   E       	          0    16404    TiposUsuarios 
   TABLE DATA           G   COPY public."TiposUsuarios" (id_tipo_usuario, tipo_nombre) FROM stdin;
    public          postgres    false    212   ?       
          0    16407    Usuarios 
   TABLE DATA           v   COPY public."Usuarios" (id_codigo, usuario_nombre, usuario_nickname, usuario_contrasena, id_tipo_usuario) FROM stdin;
    public          postgres    false    213   ?       l           2606    16411    Alumnos Alumnos_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public."Alumnos"
    ADD CONSTRAINT "Alumnos_pkey" PRIMARY KEY (id_alumno);
 B   ALTER TABLE ONLY public."Alumnos" DROP CONSTRAINT "Alumnos_pkey";
       public            postgres    false    209            p           2606    16413    Clases Clases_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public."Clases"
    ADD CONSTRAINT "Clases_pkey" PRIMARY KEY (id_clase);
 @   ALTER TABLE ONLY public."Clases" DROP CONSTRAINT "Clases_pkey";
       public            postgres    false    210            r           2606    16415    Materias Materias_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public."Materias"
    ADD CONSTRAINT "Materias_pkey" PRIMARY KEY (id_materia);
 D   ALTER TABLE ONLY public."Materias" DROP CONSTRAINT "Materias_pkey";
       public            postgres    false    211            t           2606    16417     TiposUsuarios TiposUsuarios_pkey 
   CONSTRAINT     o   ALTER TABLE ONLY public."TiposUsuarios"
    ADD CONSTRAINT "TiposUsuarios_pkey" PRIMARY KEY (id_tipo_usuario);
 N   ALTER TABLE ONLY public."TiposUsuarios" DROP CONSTRAINT "TiposUsuarios_pkey";
       public            postgres    false    212            v           2606    16419    Usuarios Usuarios_pkey 
   CONSTRAINT     _   ALTER TABLE ONLY public."Usuarios"
    ADD CONSTRAINT "Usuarios_pkey" PRIMARY KEY (id_codigo);
 D   ALTER TABLE ONLY public."Usuarios" DROP CONSTRAINT "Usuarios_pkey";
       public            postgres    false    213            m           1259    16420    fki_idClase    INDEX     G   CREATE INDEX "fki_idClase" ON public."Alumnos" USING btree (id_clase);
 !   DROP INDEX public."fki_idClase";
       public            postgres    false    209            n           1259    16421    fki_idMateria    INDEX     K   CREATE INDEX "fki_idMateria" ON public."Alumnos" USING btree (id_materia);
 #   DROP INDEX public."fki_idMateria";
       public            postgres    false    209            w           1259    16422    fki_id_tipoUsuario    INDEX     V   CREATE INDEX "fki_id_tipoUsuario" ON public."Usuarios" USING btree (id_tipo_usuario);
 (   DROP INDEX public."fki_id_tipoUsuario";
       public            postgres    false    213            x           2606    16423    Alumnos idClase    FK CONSTRAINT     |   ALTER TABLE ONLY public."Alumnos"
    ADD CONSTRAINT "idClase" FOREIGN KEY (id_clase) REFERENCES public."Clases"(id_clase);
 =   ALTER TABLE ONLY public."Alumnos" DROP CONSTRAINT "idClase";
       public          postgres    false    3184    209    210            y           2606    16428    Alumnos idMateria    FK CONSTRAINT     ?   ALTER TABLE ONLY public."Alumnos"
    ADD CONSTRAINT "idMateria" FOREIGN KEY (id_materia) REFERENCES public."Materias"(id_materia);
 ?   ALTER TABLE ONLY public."Alumnos" DROP CONSTRAINT "idMateria";
       public          postgres    false    3186    209    211            z           2606    16433    Usuarios id_tipoUsuario    FK CONSTRAINT     ?   ALTER TABLE ONLY public."Usuarios"
    ADD CONSTRAINT "id_tipoUsuario" FOREIGN KEY (id_tipo_usuario) REFERENCES public."TiposUsuarios"(id_tipo_usuario);
 E   ALTER TABLE ONLY public."Usuarios" DROP CONSTRAINT "id_tipoUsuario";
       public          postgres    false    212    3188    213               >   x?s4000??M,J?/?4?NN_??)?3?4?r?? E????0?iRch Vc????? ???         )   x?s6000?t3?r2L9?L?sN730Â?͘+F??? ?s?         <   x??5000?(?O/J?ML?????5004?,=?6739?50?t???K,?L?????? ?E?      	   <   x?1000?tL????,.)JL?/?
?q:R?JR?|c?????ԒĢ?|?=... ?6      
   B   x?5000?tN,??/?LR?%@????	gH?+Hs??!H?,b?陛?o?ʍ?b???? ?N?     