using UnityEngine;
using System.Collections;
using System;

public static class Gauss
{

    public static float[,] GenerateFalloffMap(int size, int gauss_size, int num_gauss, float scale)
    {
        float[,] map = new float[size, size];
        GaussGenerator[] normal_maps = new GaussGenerator[num_gauss];

        for (int k = 0; k < num_gauss; k += 1)
        {
            GaussGenerator generator = new GaussGenerator(gauss_size, scale);
            normal_maps[k] = generator;
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                float x = (i / (float)size) * gauss_size;
                float y = j / (float)size * gauss_size;
                float value = 0;

                for (int z = 0; z < num_gauss; z++)
                {
                    value += normal_maps[z].GetProbability(x, y, scale);
                }

                map[i, j] = value;
            }
        }
        return map;
    }
}

class GaussGenerator
{
    private float[] means = new float[2];
    private float[] cov = new float[4];
    private System.Random random = new System.Random();

    public GaussGenerator(float length, float scale)
    {
        //Generate random means
        var mean_x = (float)random.NextDouble() * length;
        var mean_y = (float)random.NextDouble() * length;
        means[0] = mean_x; means[1] = mean_y;
        //Generate random covariances, with spread modified by scale
        var var_x = (float)random.NextDouble();
        var var_y = (float)random.NextDouble();
        //Calculate cov_xy
        float correlation = (float)random.NextDouble();
        float cov_xy = (float)Math.Sqrt(var_x * var_y) * correlation;

        cov[0] = 0.1f; cov[1] = 0;
        cov[2] = 0; cov[3] = 0.1f;
    }

    public float GetProbability(float x, float y, float scale)
    {
        float determinant = cov[0] * cov[3] - cov[1] * cov[2];

        float inverse_cov_coef = (1 / determinant);

        float[] inverse_cov_matrix = new float[4] { inverse_cov_coef * cov[3], inverse_cov_coef * (-cov[1]), inverse_cov_coef * (-cov[2]), inverse_cov_coef * cov[0] };
        float matrix_product = (x - means[0]) * (inverse_cov_matrix[0] * (x - means[0]) + inverse_cov_matrix[1] * (y - means[1])) + (y - means[1]) *
            (inverse_cov_matrix[2] * (x - means[0]) + inverse_cov_matrix[3] * (y - means[1]));

        float prob = (1 / (2 * (float)Math.PI * (float)Math.Sqrt(determinant))) * (float)Math.Exp(-matrix_product / 2);

        return prob*scale;
    }
}
