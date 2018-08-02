using Chess.Figures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess {
    public class ChessBoard960: ChessBoard {
        Random rand = new Random();
        char[] otherFiguresPlaces = null;

        Figure[] whitePawns, whiteFigures, blackPawns, blackFigures;

        public ChessBoard960():base(){
            whitePawns = "ABCDEFGH".Select(charPositionPawn => new Pawn(charPositionPawn, 7, true)).ToArray();
            blackPawns = "ABCDEFGH".Select(charPositionPawn => new Pawn(charPositionPawn, 2, false)).ToArray();

            otherFiguresPlaces = "RNBQKBNR".ToCharArray();
            bool valid = false;
            do {
                rand.Shuffle(otherFiguresPlaces);

                int[] bishopIndexes = Enumerable.Range(0, otherFiguresPlaces.Length).Where(x => otherFiguresPlaces[x] == 'B').ToArray();
                valid = (bishopIndexes.Length == 2) &&
                        ((bishopIndexes[0] % 2) != (bishopIndexes[1] % 2));

                int[] rookIndexes = Enumerable.Range(0, otherFiguresPlaces.Length).Where(x => otherFiguresPlaces[x] == 'R').ToArray();
                int kingIndex = Enumerable.Range(0, otherFiguresPlaces.Length).Where(x => otherFiguresPlaces[x] == 'K').Single();
                valid = valid && (rookIndexes[0] < kingIndex && rookIndexes[1] > kingIndex);
            } while(!valid);

            blackFigures = Enumerable.Range(0, 8)
                                        .Select(x => new Tuple<int, char, char, bool>(1, (char)(x + 'A'), otherFiguresPlaces[x], false))
                                        .Select(x => {
                                            Figure ret = null;
                                            switch(x.Item3) {
                                                case 'R':
                                                    ret = new Rook(x.Item2, x.Item1, x.Item4);
                                                    break;
                                                case 'N':
                                                    ret = new Knight(x.Item2, x.Item1, x.Item4);
                                                    break;
                                                case 'B':
                                                    ret = new Bishop(x.Item2, x.Item1, x.Item4);
                                                    break;
                                                case 'K':
                                                    ret = new King(x.Item2, x.Item1, x.Item4);
                                                    break;
                                                case 'Q':
                                                    ret = new Queen(x.Item2, x.Item1, x.Item4);
                                                    break;
                                            }
                                            return ret;
                                        }).ToArray();

            whiteFigures = Enumerable.Range(0, 8)
                                         .Select(x => new Tuple<int, char, char, bool>(8, (char)(x + 'A'), otherFiguresPlaces[x], true))
                                         .Select(x => {
                                             Figure ret = null;
                                             switch(x.Item3) {
                                                 case 'R':
                                                     ret = new Rook(x.Item2, x.Item1, x.Item4);
                                                     break;
                                                 case 'N':
                                                     ret = new Knight(x.Item2, x.Item1, x.Item4);
                                                     break;
                                                 case 'B':
                                                     ret = new Bishop(x.Item2, x.Item1, x.Item4);
                                                     break;
                                                 case 'K':
                                                     ret = new King(x.Item2, x.Item1, x.Item4);
                                                     break;
                                                 case 'Q':
                                                     ret = new Queen(x.Item2, x.Item1, x.Item4);
                                                     break;
                                             }
                                             return ret;
                                         }).ToArray();
        }

        public override void placeBlackFigures() {
            for(int i = 0; i < 8; i++) {
                placeFigure(blackPawns[i]);
                placeFigure(blackFigures[i]);
            }
        }
        public override void placeWhiteFigures() {
            for(int i = 0; i < 8; i++) {
                placeFigure(whitePawns[i]);
                placeFigure(whiteFigures[i]);
            }
        }
    }
}
