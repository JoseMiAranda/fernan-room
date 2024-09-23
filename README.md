<div align="center" style="background-color:">
  <img src="documentation/logo.png" width="300px" alt="download">
</div>
<br><br>

- [Galería](#galería)
- [Introducción](#introducción)
- [Controles](#controles)
- [¿Cómo lo descargarlo?](#cómo-lo-descargarlo)
- [¿Cómo modificar el proyecto?](#cómo-modificar-el-proyecto)
- [¿Cómo exportarlo?](#cómo-exportarlo)
- [Recursos](#recursos)

## Galería

<table>
  <tbody>
     <tr>
        <td>
            <img src="documentation/preview_1.png" width="400px" alt="preview_1">
        </td>
        <td>
            <img src="documentation/preview_2.png" width="400px" alt="preview_2">
        </td>
    </tr>
    <tr>
        <td>
            <img src="documentation/preview_3.png" width="400px" alt="preview_3">
        </td>
        <td>
            <img src="documentation/preview_4.png" width="400px" alt="preview_4">
        </td>
    </tr>
  </tbody>
</table>

## Introducción

El videojuego "oficial" del [I.E.S Fernando III](https://web.iesfernandoiii.es/) con temática Escape Room está ahora disponible. Sumérgete en un aula llena de diversos enigmas y ayuda tanto a maestros como alumnos.
El aula es una recreación del aula de 2ºDAM del año 2023 - 2024. Para los antiguos estudiantes sed libres de destrozarla, para los nuevos también.

## Controles

<table border="1">
  <tbody>
     <tr>
        <td>
          Ratón
        </td>
        <td>
          Mirar
        </td>
    </tr>  
    <tr>
        <td>
          Rueda del Ratón
        </td>
        <td>
          Acercar / Alejar objetos
        </td>
    </tr>
     <tr>
        <td>
          A, W, S , D
        </td>
        <td>
          Desplazarse
        </td>
    </tr>
    <tr>
        <td>
          E
        </td>
        <td>
          Agarrar / Interactuar con objetos 
        </td>
    </tr>
    <tr>
        <td>
          Q
        </td>
        <td>
          Reiniciar objetos
        </td>
    </tr>
    <tr>
        <td>
          Espacio
        </td>
        <td>
          Saltar
        </td>
    </tr>
    <tr>
        <td>
          Clic izquierdo
        </td>
        <td>
          Disparar (Ronda 2)
        </td>
    </tr>
    <tr>
        <td>
          Clic derecho 
        </td>
        <td>
          Acercar / Alejar espejo (Ronda 5)
        </td>
    </tr>
  </tbody>
</table>

## ¿Cómo lo descargarlo?

Decargue o clone el repositorio. Si clona el repositorio, asegúrate de tener instalado [Git LFS](https://git-lfs.com/), para ello sigue estos pasos:

```cmd
git clone https://github.com/JoseMiAranda/fernan-room.git
git lfs install
git config --global core.autocrlf true
```

> ⚠️ Es necesario tener instalado Git LFS para manejar los archivos de Unity definidos en el .gitattributes. Si necesitas actualizarlo sigue los pasos del siguiente [enlace](https://docs.github.com/es/get-started/getting-started-with-git/configuring-git-to-handle-line-endings)

<img src="documentation/download.png" width="500px" alt="download">
<br><br>

Accede a la carpeta donde lo descargaste, en ella accede a la carpeta **build** que te tendrá todo lo indispensable para jugar al juego.
Pulsa sobre el ejecutable.

<img src="documentation/game.png" width="500px" alt="game">

## ¿Cómo modificar el proyecto?

Se necesita tener una cuenta de [Unity](https://unity.com/es) y [UnityHub](https://unity.com/es/unity-hub) instalado.

Desde UnityHub inicia sesión y desde el apartado de **Projects** accede a **Add > Add Project from disk**. Busque la ruta del repositorio.

<img src="documentation/added.png" width="500px" alt="added">
<br><br>

Este proyecto se ha realizado con Unity 2022.3.2f1. Si usted no lo tiene instalado, te mostrará un panel de instalación.
Una vez instalado, se abrirá el editor, es normal que tarde la primera vez que se abra el proyecto.

<img src="documentation/editor.png" width="500px" alt="editor">
<br><br>

Si no se puede instalar el editor automáticamente, puedes descargarlo desde [Unity 2022.3.21f1](https://unity.com/es/releases/editor/whats-new/2022.3.21#notes) seleccionando la plataforma adecuada.

En **Installs**, pulse en **Locale** y busque la carpeta de **Editor** de Unity 2022.3.21f1. De esta manera se agregará.

<img src="documentation/locale.png" width="600px" alt="locale">
<br><br>

Una vez abierto, aparecerá un mundo vacío, para acceder al mapa del aula accede a y pulsa sobre **Main**. Si se ve el aula, solamente falta pulsar sobre ▶️ para ejecutarlo.

<img src="documentation/main.png" width="600px" alt="main">
<br><br>

## ¿Cómo exportarlo?

Dirígete a **File > Build Settings**

<img src="documentation/build-settings.png" width="300px" alt="build settings">
<br><br>
<img src="documentation/build.png" width="500px" alt="build">
<br><br>
Seleccione la ruta donde se generaá el build.
<br><br>
<img src="documentation/game.png" width="500px" alt="game">

## Recursos

<table border="1" width="500px">
  <thead>
    <tr >
      <th colspan="2">Modelos</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="width: 100px;">Llave</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/handpainted-keys-42044">https://assetstore.unity.com/packages/3d/handpainted-keys-42044</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Audiencia</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/characters/humanoids/humans/audience-crowd-8563">https://assetstore.unity.com/packages/3d/characters/humanoids/humans/audience-crowd-8563</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Comida</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/props/food/low-poly-food-lite-258693">https://assetstore.unity.com/packages/3d/props/food/low-poly-food-lite-258693</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Escuela</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/environments/school-assets-146253">https://assetstore.unity.com/packages/3d/environments/school-assets-146253</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Decoración</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/environments/urban/low-poly-storage-pack-101732">https://assetstore.unity.com/packages/3d/environments/urban/low-poly-storage-pack-101732</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Equipo informático</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/environments/lowpoly-server-room-props-197268">https://assetstore.unity.com/packages/3d/environments/lowpoly-server-room-props-197268</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Pistola</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/props/guns/sci-fi-futuristic-hand-gun-90249">https://assetstore.unity.com/packages/3d/props/guns/sci-fi-futuristic-hand-gun-90249</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Coches</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/vehicles/land/low-poly-cars-101798">https://assetstore.unity.com/packages/3d/vehicles/land/low-poly-cars-101798</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Basura</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/trash-low-poly-cartoon-pack-66229">https://assetstore.unity.com/packages/3d/trash-low-poly-cartoon-pack-66229</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Decoración</td>
      <td style="width: 400px;"><a href="https://assetstore.unity.com/packages/3d/environments/low-poly-office-props-lite-131438">https://assetstore.unity.com/packages/3d/environments/low-poly-office-props-lite-131438</a></td>
    </tr>
    
  </tbody>
  <thead>
    <tr >
      <th colspan="2">Audio</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="width: 100px;">Introducción</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/ambiente-ibiza-waves-ambient-chill-out-music-3897/">https://pixabay.com/es/music/ambiente-ibiza-waves-ambient-chill-out-music-3897/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Ronda 1</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/musica-de-elevador-elevator-music-bossa-nova-background-music-version-60s-10900/">https://pixabay.com/es/music/musica-de-elevador-elevator-music-bossa-nova-background-music-version-60s-10900/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Ronda 2</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/rock-call-the-police-18554/">https://pixabay.com/es/music/rock-call-the-police-18554/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Ronda 3</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/jazz-tradicional-food-show-163665/">https://pixabay.com/es/music/jazz-tradicional-food-show-163665/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Ronda 4</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/canguelo-funky-teaser-introduction-funk-background-music-intro-theme-120446/">https://pixabay.com/es/music/canguelo-funky-teaser-introduction-funk-background-music-intro-theme-120446/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Ronda 5</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/metal-metal-blues-120-bpm-loop-15519/">https://pixabay.com/es/music/metal-metal-blues-120-bpm-loop-15519/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Despedida</td>
      <td style="width: 400px;"><a href="https://pixabay.com/es/music/late-flickering-trees-16002/">https://pixabay.com/es/music/late-flickering-trees-16002/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Grito 1</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/Reitanna/sounds/235142/">https://freesound.org/people/Reitanna/sounds/235142/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Grito 2</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/Reitanna/sounds/344038/">https://freesound.org/people/Reitanna/sounds/344038/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Grito 3</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/Reitanna/sounds/344038/">https://freesound.org/people/Reitanna/sounds/344038/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Grito 4</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/JCH321/sounds/66204/">https://freesound.org/people/JCH321/sounds/66204/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Disparo</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/Quonux/sounds/166418/">https://freesound.org/people/Quonux/sounds/166418/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Salto</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/jalastram/sounds/386616/">https://freesound.org/people/jalastram/sounds/386616/</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Explosión</td>
      <td style="width: 400px;"><a href="https://freesound.org/people/SamsterBirdies/sounds/580040/">https://freesound.org/people/SamsterBirdies/sounds/580040/</a></td>
    </tr>
  </tbody>
   <thead>
    <tr >
      <th colspan="2">Canales de referencia</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td style="width: 100px;">Luis Canary</td>
      <td style="width: 400px;"><a href="https://www.youtube.com/@LuisCanary">https://www.youtube.com/@LuisCanary</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Guinxu</td>
      <td style="width: 400px;"><a href="https://www.youtube.com/@Guinxu">https://www.youtube.com/@Guinxu</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">Code Monkey</td>
      <td style="width: 400px;"><a href="https://www.youtube.com/@CodeMonkeyUnity">https://www.youtube.com/@CodeMonkeyUnity</a></td>
    </tr>
    <tr>
      <td style="width: 100px;">SpeedTutor</td>
      <td style="width: 400px;"><a href="https://www.youtube.com/@SpeedTutor">https://www.youtube.com/@SpeedTutor</a></td>
    </tr>
  </tbody>
</table>
