<h1>Toy Game</h1>
<h3>Produzido por Igor Cafazzi, Guilherme Santos, Matheus Henrique e Igor Michelini</h3>

<h2>Filmes Inspiração</h2>
<ul>
  <li>Toy Story</li>
  <li>Flow</li>
  <li>Extraordinário</li>
  <li>Sonhos Roubados</li>
</ul>

<h2>Diagrama de Classes</h2>
<img src="./DiagramaMostra.png">
<h3>Uso de Caso</h3>
<h4>Personagem</h4>
A classe utilizada para os personagens dentro do jogo, se comportarão diferente baseado no seu tipo (Jogador, Inimigo, Chefe). <br>
Nela são armazenadas seus atributos, como vida, dano, energia, velocidade e posição. <br>
Também contém uma referência ao Animator para tocar animações usando seu método Animacao. Também possui métodos para mover, atacar, defender e curar vida. 

<h4>Sistema</h4>
Gerencia o fluxo geral do jogo. Contendo uma referência a todas as fases e a todos os personagens presentes nela. <br>
Tem métodos para fechar o jogo, trocar de fase e de derrota caso o jogador perca. <br>
O método Nascimento serve para checar os personagens presentes na fase, assim é capaz de evitar situações como haver dois jogadores ou inimigos demais em uma fase. 

<h4>Fase</h4>
Uma classe simples, contém uma referência a seu tema de fundo, seu tileset (modelosFase) e a todos os interagíveis presentes nela.
Utiliza do método TocarMusica para tocar seu tema de fundo.
