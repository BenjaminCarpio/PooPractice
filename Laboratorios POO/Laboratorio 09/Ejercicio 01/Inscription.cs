﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace Ejercicio_01
{
    public partial class Inscription : UserControl
    {
        public Inscription()
        {
            InitializeComponent();
        }

        private void Inscription_Load_1(object sender, EventArgs e)
        {
            var assignatures = ConnectionDB.ExecuteQuery("SELECT nombre FROM materia");
            var assignaturesCombo = new List<string>();

            
            foreach (DataRow dr in assignatures.Rows)
            {
                assignaturesCombo.Add(dr[0].ToString());
            }

            comboBox1.DataSource = assignaturesCombo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(""))
            {
                MessageBox.Show("No se permite dejar campos vacios");
            }
            else
            {
                try
                {
                    string query = $"SELECT idMateria FROM materia WHERE nombre = '{comboBox1.SelectedItem.ToString()}'";

                    var dt = ConnectionDB.ExecuteQuery(query);
                    var dr = dt.Rows[0];
                    var idMateria = Convert.ToInt32(dr[0].ToString());
                    
                    string nonQuery = $"INSERT INTO INSCRIPCION(idMateria, carnet) VALUES(" +
                                      $"{idMateria}," +
                                      $"'{textBox1.Text}')";
                    
                    ConnectionDB.ExecuteNonQuery(nonQuery);

                    MessageBox.Show("Se ha inscrito la materia");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ha ocurrido un error");
                }
            }
        }
    }
}