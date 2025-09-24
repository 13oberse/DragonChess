# DragonChess
A Blazor-server based implmementation of the game dragonchess (from Dungeons & Dragons).

## How to play
See https://www.chessvariants.com/3d.dir/dragonchess.html for a detailed description of the rules

Clicking any piece to select it will highlight it in green, and highlight the cells it can move to in yellow. If an enemy piece is highlighted in yellow, that means the piece you highlighted can capture that piece by moving to it. The Dragon can also remotely capture pieces (capture a piece without moving), those pieces will be highlighted in red.

If it's your turn, clicking a cell highlighted in yellow will move the selected piece to that cell, and clicking a cell highlighted in red will remotely capture that piece. After either of these actions, it will switch to the other player's turn.

Regardless of turn, clicking the selected piece again (or clicking any non-highlighted cell) will de-select that piece.

If you highlight a piece and it unexpectedly can't move anywhere, check the space directly below it; the Basilisk piece can freeze enemy pieces that are directly above it (preventing them from being able to move or capture).

## Spectators
If more than two people attempt to connect to the game, everyone beyond the first two will connect as spectators. This just means they can watch the game without any control over it.

## Reset Button
If you're a current player, clicking this button will reset the board to the starting configuration. If you're a spectator, this button will only reset the board if one of the players has left the game (in which case you will also take their spot as a player).

## P2 perspective button
This button will flip your perspective as if you walked to the other side of the board, in case you want to view the board from the other perspective. The default view is from the white player's perspective for everyone except the black player.

## Current features
- Basic game functionality: piece movement, players can move on their turns, etc.
- Game knows whether a player is in check, though it currently doesn't act on this or notify the players (other than through the console)

## Planned/potential features
- Check for checkmate (planned)
- Notify players of check and mate (planned)
- Limit player moves if in check
- Add a way for a spectator to join the game without reseting the board (nice to have, shouldn't be difficult to implement)
- Player-facing log of moves (probably a console that would also let you know of check/mate conditions)
- Picture icons instead of letters for pieces?
- Ability to switch icon sets?
- Animate moves?
