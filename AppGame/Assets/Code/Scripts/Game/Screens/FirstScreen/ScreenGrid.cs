using UnityEngine;

public class ScreenGrid : MonoBehaviour
{
    [SerializeField] private int rows = 10; // Número de linhas do grid
    [SerializeField] private int columns = 10; // Número de colunas do grid
    [SerializeField] private Color gridColor = Color.red; // Cor das linhas do grid

    void OnDrawGizmos()
    {
        Camera cam = Camera.main;

        if (cam == null)
            return;

        // Determina o tamanho da tela em pixels
        float screenWidth = cam.pixelWidth;
        float screenHeight = cam.pixelHeight;

        // Calcula o tamanho de cada célula do grid
        float cellWidth = screenWidth / columns;
        float cellHeight = screenHeight / rows;

        // Calcula o canto inferior esquerdo da tela em coordenadas do mundo
        Vector3 bottomLeft = new(-screenWidth / 2, -screenHeight / 2, 0);

        // Desenha as linhas horizontais do grid
        Gizmos.color = gridColor;

        for (int i = 0; i <= rows; i++)
        {
            float y = i * cellHeight;
            Vector3 startPoint = transform.TransformPoint(bottomLeft + new Vector3(0, y, 0));
            Vector3 endPoint = transform.TransformPoint(bottomLeft + new Vector3(screenWidth, y, 0));
            Gizmos.DrawLine(startPoint, endPoint);
        }

        // Desenha as linhas verticais do grid
        for (int i = 0; i <= columns; i++)
        {
            float x = i * cellWidth;
            Vector3 startPoint = transform.TransformPoint(bottomLeft + new Vector3(x, 0, 0));
            Vector3 endPoint = transform.TransformPoint(bottomLeft + new Vector3(x, screenHeight, 0));
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}
