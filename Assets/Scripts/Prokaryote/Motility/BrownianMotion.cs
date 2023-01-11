using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class BrownianMotion
{
    public static ReturnBrown MoveBody(Vector3[] vertices, float vertices_mass, Vector3 center, float stokes_rad, float rho, float molar_mass, float critical_temperature, float temperature, float concentration, float time_step, int estimate)
    {
        float boltzman = 1.380649f * (float)Math.Pow(10, -23); // m2 kg / s2 K1 - boltzman
        float avogadro = 6.023f * (float)Math.Pow(10, 23); // 1 / mol - avogadro
        float planck = 6.62607015f * (float)Math.Pow(10, -34); // m2 kg / s1 - planck
        float volume = molar_mass / rho;
        int run_t = 0;

        float viscosity = (avogadro * planck * (float)Math.Exp(3.8f * (critical_temperature / temperature))) / volume; // viscosity
        float diffusion = (3 * boltzman * temperature) / (6f * (float)Math.PI * viscosity * stokes_rad); // Diffusion coefficient, multiplied by three for better fitting
        float a_avg = (3 / 2) * (float)Math.Sqrt(diffusion / (float)Math.PI) * (float)Math.Pow(time_step, -3 / 2); // a avg
        float n_col = (4 * boltzman * temperature * concentration * time_step) / (3 * viscosity); // number of collisions, additionaly divided by two as only one side is bombarded

        System.Random random = new System.Random();

        Vector3 transposition_a = new Vector3(0, 0, 0);
        Vector3 angular_a = new Vector3(0, 0, 0);
        Vector3 net_torque = new Vector3(0, 0, 0);
        float inertia = 0;

        for (int i = 0; i < vertices.Count(); i++)
        {
            Vector3 run_force = new Vector3(0, 0, 0);

            for (int j = 0; j < estimate; j++)
            {
                float theta = (float)Math.Acos(1 - 2 * random.NextDouble());
                float phi = (float)Math.Acos(1 - 2 * random.NextDouble());


                float s = (float)Math.Sin(phi) * (float)Math.Cos(theta);
                float y = (float)Math.Sin(phi) * (float)Math.Sin(theta);
                float z = (float)Math.Cos(phi);


                run_force.x += s * (n_col / estimate);
                run_force.y += y * (n_col / estimate);
                run_force.z += z * (n_col / estimate);
            }

            run_force.x *= (molar_mass / avogadro) * a_avg;
            run_force.y *= (molar_mass / avogadro) * a_avg;
            run_force.z *= (molar_mass / avogadro) * a_avg;

            //Compute transformation and rotational vectors
            Vector3 center_unit = (center - vertices[i]);
            center_unit = center_unit / center_unit.magnitude;
            Vector3 force_unit = run_force / run_force.magnitude;
            float force_angle = (float)Math.Acos(force_unit.x * center_unit.x + force_unit.y * center_unit.y + force_unit.z * center_unit.z);
            Vector3 cross_unit = Vector3.Cross(force_unit, center_unit);
            cross_unit = cross_unit / cross_unit.magnitude;

            float K = (-cross_unit.x * center_unit.y + cross_unit.y * center_unit.x);
            float M = (-cross_unit.x * center_unit.z + cross_unit.z * center_unit.x);
            float S = (-center_unit.y * M + K * center_unit.z) / (M * cross_unit.x);
            float T = -1 + (float)Math.Pow(K, 2) / (float)Math.Pow(M, 2);

            float R2 = (float)Math.Sqrt(1 / ((float)Math.Pow(S, 2)) - T);

            Vector3 rotate_unit = new Vector3(S * R2, R2, (-R2 * K) / M);

            float center_magnitude = (float)Math.Cos(force_angle) * run_force.magnitude;
            float rotate_magnitude = (float)Math.Sin(force_angle) * run_force.magnitude;

            center_unit = center_unit * center_magnitude;

            net_torque += Vector3.Cross((center - vertices[i]), run_force); //Torque
            inertia += vertices_mass * (float)Math.Pow((center - vertices[i]).magnitude, 2);

            transposition_a.x += center_unit.x / (vertices_mass * vertices.Count()); transposition_a.y += center_unit.y / (vertices_mass * vertices.Count()); transposition_a.z += center_unit.z / (vertices_mass * vertices.Count());
        }

        angular_a = net_torque / inertia;
        ReturnBrown ret = new ReturnBrown();

        ret.transposition_a = transposition_a;
        ret.angular_a = angular_a;

        return ret;

    }

    static float DragCoefficient()
    {
        //In future, after lattice boltzman and euler finished, should be used to calculate true drag coef
        //This equation is summarized with all values necessary
        return 0.47f;
    }

    static float CrossSectionalArea()
    {
        //In future, after lattice boltzman and euler finished, should be used to calculate true cross sectional area
        //This equation will be estimated as closest, largest area of movement in liquid direction
        //It will be plane intersection of longest axis of symmetry (need to find how to define) at point with highest area (also need to define)
        return (float)Math.PI * (float)Math.Pow(1, 2);
    }

    public static Vector3 Drag(float density, Vector3 object_velocity, Vector3 fluid_velocity)
    {
        float drag_coef = DragCoefficient();
        float cross_area = CrossSectionalArea();

        Vector3 relative_velocity = object_velocity - fluid_velocity;
        Vector3 drag_force = new Vector3();
        drag_force.x = (1 / 2) * density * (float)Math.Pow(relative_velocity.x, 2) * drag_coef * cross_area;
        drag_force.y = (1 / 2) * density * (float)Math.Pow(relative_velocity.y, 2) * drag_coef * cross_area;
        drag_force.z = (1 / 2) * density * (float)Math.Pow(relative_velocity.z, 2) * drag_coef * cross_area;

        return drag_force;
    }
}
