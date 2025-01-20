using UnityEngine;


/// <summary>
/// Class for helper methods for TileDetails
/// </summary>
public static class TileDetailsUtils{


    /// <summary>
    /// Method to clean the wall sprite name so that it can be used to find the correct sprite in the sprite dictionary
    /// </summary>
    /// <param name="wallSpriteName"></param>
    /// <param name="nameBeforeNumbers"></param>
    /// <returns></returns>
    public static string CleanWallSpriteName(string wallSpriteName, string nameBeforeNumbers){

        //standalone walls
        if(wallSpriteName == nameBeforeNumbers + "0" || wallSpriteName == nameBeforeNumbers + "02" || wallSpriteName == nameBeforeNumbers + "024" || wallSpriteName == nameBeforeNumbers + "0246"
        || wallSpriteName == nameBeforeNumbers + "026" || wallSpriteName == nameBeforeNumbers + "04" || wallSpriteName == nameBeforeNumbers + "046" || wallSpriteName == nameBeforeNumbers + "06"
        || wallSpriteName == nameBeforeNumbers + "2" || wallSpriteName == nameBeforeNumbers + "24" || wallSpriteName == nameBeforeNumbers + "246" || wallSpriteName == nameBeforeNumbers + "26"
        || wallSpriteName == nameBeforeNumbers + "4" || wallSpriteName == nameBeforeNumbers + "46" || wallSpriteName == nameBeforeNumbers + "6"){
            return nameBeforeNumbers; // standalone wall
        }
        // walls facing a single direction
        if(wallSpriteName == nameBeforeNumbers + "05" || wallSpriteName == nameBeforeNumbers + "25" || wallSpriteName == nameBeforeNumbers + "45" || wallSpriteName == nameBeforeNumbers + "56"
        || wallSpriteName == nameBeforeNumbers + "025" || wallSpriteName == nameBeforeNumbers + "045" || wallSpriteName == nameBeforeNumbers + "056" || wallSpriteName == nameBeforeNumbers + "245"
        || wallSpriteName == nameBeforeNumbers + "256" || wallSpriteName == nameBeforeNumbers + "456" || wallSpriteName == nameBeforeNumbers + "0245" || wallSpriteName == nameBeforeNumbers + "0256"
        || wallSpriteName == nameBeforeNumbers + "0456" || wallSpriteName == nameBeforeNumbers + "2456" || wallSpriteName == nameBeforeNumbers + "02456"){
            return nameBeforeNumbers + "5"; // wall facing south
        }
        if(wallSpriteName == nameBeforeNumbers + "07" || wallSpriteName == nameBeforeNumbers + "27" || wallSpriteName == nameBeforeNumbers + "47" || wallSpriteName == nameBeforeNumbers + "67"
        || wallSpriteName == nameBeforeNumbers + "027" || wallSpriteName == nameBeforeNumbers + "047" || wallSpriteName == nameBeforeNumbers + "067" || wallSpriteName == nameBeforeNumbers + "247"
        || wallSpriteName == nameBeforeNumbers + "267" || wallSpriteName == nameBeforeNumbers + "467" || wallSpriteName == nameBeforeNumbers + "0247" || wallSpriteName == nameBeforeNumbers + "0267"
        || wallSpriteName == nameBeforeNumbers + "0467" || wallSpriteName == nameBeforeNumbers + "2467" || wallSpriteName == nameBeforeNumbers + "02467"){
            return nameBeforeNumbers + "7"; // wall facing west
        }
        if(wallSpriteName == nameBeforeNumbers + "01" || wallSpriteName == nameBeforeNumbers + "12" || wallSpriteName == nameBeforeNumbers + "14" || wallSpriteName == nameBeforeNumbers + "16"
        || wallSpriteName == nameBeforeNumbers + "012" || wallSpriteName == nameBeforeNumbers + "014" || wallSpriteName == nameBeforeNumbers + "016" || wallSpriteName == nameBeforeNumbers + "124"
        || wallSpriteName == nameBeforeNumbers + "126" || wallSpriteName == nameBeforeNumbers + "146" || wallSpriteName == nameBeforeNumbers + "0124" || wallSpriteName == nameBeforeNumbers + "0126"
        || wallSpriteName == nameBeforeNumbers + "0146" || wallSpriteName == nameBeforeNumbers + "1246" || wallSpriteName == nameBeforeNumbers + "01246"
        ){
            return nameBeforeNumbers + "1"; // wall facing north
        }
        if(wallSpriteName == nameBeforeNumbers + "03" || wallSpriteName == nameBeforeNumbers + "23" || wallSpriteName == nameBeforeNumbers + "34" || wallSpriteName == nameBeforeNumbers + "36"
        || wallSpriteName == nameBeforeNumbers + "023" || wallSpriteName == nameBeforeNumbers + "034" || wallSpriteName == nameBeforeNumbers + "036" || wallSpriteName == nameBeforeNumbers + "234"
        || wallSpriteName == nameBeforeNumbers + "236" || wallSpriteName == nameBeforeNumbers + "346" || wallSpriteName == nameBeforeNumbers + "0234" || wallSpriteName == nameBeforeNumbers + "0236"
        || wallSpriteName == nameBeforeNumbers + "0346" || wallSpriteName == nameBeforeNumbers + "2346" || wallSpriteName == nameBeforeNumbers + "02346"){
            return nameBeforeNumbers + "3"; // wall facing east
        }

        //Straight Walls
        //East/West wall
        if(wallSpriteName == nameBeforeNumbers + "037" || wallSpriteName == nameBeforeNumbers + "237" || wallSpriteName == nameBeforeNumbers + "347" || wallSpriteName == nameBeforeNumbers + "367"
        || wallSpriteName == nameBeforeNumbers + "0237" || wallSpriteName == nameBeforeNumbers + "0347" || wallSpriteName == nameBeforeNumbers + "0367" || wallSpriteName == nameBeforeNumbers + "2347"
        || wallSpriteName == nameBeforeNumbers + "2367" || wallSpriteName == nameBeforeNumbers + "3467" || wallSpriteName == nameBeforeNumbers + "02347" || wallSpriteName == nameBeforeNumbers + "02367"
        || wallSpriteName == nameBeforeNumbers + "03467" || wallSpriteName == nameBeforeNumbers + "23467" || wallSpriteName == nameBeforeNumbers + "023467"){
            return nameBeforeNumbers + "37"; // centre wall east to west
        }
        //North/South wall
        if(wallSpriteName == nameBeforeNumbers + "015" || wallSpriteName == nameBeforeNumbers + "125" || wallSpriteName == nameBeforeNumbers + "145" || wallSpriteName == nameBeforeNumbers + "156"
        || wallSpriteName == nameBeforeNumbers + "0125" || wallSpriteName == nameBeforeNumbers + "0145" || wallSpriteName == nameBeforeNumbers + "0156" || wallSpriteName == nameBeforeNumbers + "1245"
        || wallSpriteName == nameBeforeNumbers + "1256" || wallSpriteName == nameBeforeNumbers + "1456" || wallSpriteName == nameBeforeNumbers + "01245" || wallSpriteName == nameBeforeNumbers + "01256"
        || wallSpriteName == nameBeforeNumbers + "01456" || wallSpriteName == nameBeforeNumbers + "12456" || wallSpriteName == nameBeforeNumbers + "012456"){
            return nameBeforeNumbers + "15"; // centre wall north to south
        }

        //corners
        //South/West corner
        if(wallSpriteName == nameBeforeNumbers + "057" || wallSpriteName == nameBeforeNumbers + "257" || wallSpriteName == nameBeforeNumbers + "457" || wallSpriteName == nameBeforeNumbers + "0257"
        || wallSpriteName == nameBeforeNumbers + "0457" || wallSpriteName == nameBeforeNumbers + "2457" || wallSpriteName == nameBeforeNumbers + "02457"){
            return nameBeforeNumbers + "57"; // south west corner
        }
        //South/East corner
        if(wallSpriteName == "WallTilesAdvanced_035" || wallSpriteName == nameBeforeNumbers + "235" || wallSpriteName == nameBeforeNumbers + "356" || wallSpriteName == nameBeforeNumbers + "0235"
        || wallSpriteName == nameBeforeNumbers + "0356" || wallSpriteName == nameBeforeNumbers + "2356" || wallSpriteName == nameBeforeNumbers + "02356"){
            return nameBeforeNumbers + "35"; // south east corner
        }
        //North/West corner
        if(wallSpriteName == nameBeforeNumbers + "127" || wallSpriteName == nameBeforeNumbers + "147" || wallSpriteName == nameBeforeNumbers + "167" || wallSpriteName == nameBeforeNumbers + "1247"
        || wallSpriteName == nameBeforeNumbers + "1267" || wallSpriteName == nameBeforeNumbers + "1467" || wallSpriteName == nameBeforeNumbers + "12467"){
            return nameBeforeNumbers + "17"; // north west corner
        }
        //North/East corner
        if(wallSpriteName == nameBeforeNumbers + "013" || wallSpriteName == nameBeforeNumbers + "134" || wallSpriteName == nameBeforeNumbers + "136" || wallSpriteName == nameBeforeNumbers + "0134"
        || wallSpriteName == nameBeforeNumbers + "0136" || wallSpriteName == nameBeforeNumbers + "1346" || wallSpriteName == nameBeforeNumbers + "01346"){
            return nameBeforeNumbers + "13"; // north east corner
        }
        //filled corners
        //South/West filled corner
        if(wallSpriteName == nameBeforeNumbers + "0567" || wallSpriteName == nameBeforeNumbers + "2567" || wallSpriteName == nameBeforeNumbers + "4567" || wallSpriteName == nameBeforeNumbers + "02567"
        || wallSpriteName == nameBeforeNumbers + "04567" || wallSpriteName == nameBeforeNumbers + "24567" || wallSpriteName == nameBeforeNumbers + "024567"){
            return nameBeforeNumbers + "567"; // south west filled corner
        }
        //South/East filled corner
        if(wallSpriteName == nameBeforeNumbers + "0345" || wallSpriteName == nameBeforeNumbers + "2345" || wallSpriteName == nameBeforeNumbers + "3456" || wallSpriteName == nameBeforeNumbers + "02345"
        || wallSpriteName == nameBeforeNumbers + "03456" || wallSpriteName == nameBeforeNumbers + "23456" || wallSpriteName == nameBeforeNumbers + "023456"){
            return nameBeforeNumbers + "345"; // south east filled corner
        }
        //North/West filled corner
        if(wallSpriteName == nameBeforeNumbers + "0127" || wallSpriteName == nameBeforeNumbers + "0147" || wallSpriteName == nameBeforeNumbers + "0167" || wallSpriteName == nameBeforeNumbers + "01247"
        || wallSpriteName == nameBeforeNumbers + "01267" || wallSpriteName == nameBeforeNumbers + "01467" || wallSpriteName == nameBeforeNumbers + "012467"){
            return nameBeforeNumbers + "017"; // north west filled corner
        }
        //North/East filled corner
        if(wallSpriteName == nameBeforeNumbers + "0123" || wallSpriteName == nameBeforeNumbers + "1234" || wallSpriteName == nameBeforeNumbers + "1236" || wallSpriteName == nameBeforeNumbers + "01234"
        || wallSpriteName == nameBeforeNumbers + "01236" || wallSpriteName == nameBeforeNumbers + "12346" || wallSpriteName == nameBeforeNumbers + "012346"){
            return nameBeforeNumbers + "123"; // north east filled corner
        }

        //T junctions
        //North T junction
        if(wallSpriteName == nameBeforeNumbers + "1347" || wallSpriteName == nameBeforeNumbers + "1367" || wallSpriteName == nameBeforeNumbers + "13467"){
            return nameBeforeNumbers + "137"; // north T junction
        }
        //East T junction
        if(wallSpriteName == nameBeforeNumbers + "0135" || wallSpriteName == nameBeforeNumbers + "1356" || wallSpriteName == nameBeforeNumbers + "01356"){
            return nameBeforeNumbers + "135"; // east T junction
        }
        //South T junction
        if(wallSpriteName == nameBeforeNumbers + "0357" || wallSpriteName == nameBeforeNumbers + "2357" || wallSpriteName == nameBeforeNumbers + "02357"){
            return nameBeforeNumbers + "357"; // south T junction
        }
        //West T junction
        if(wallSpriteName == nameBeforeNumbers + "1257" || wallSpriteName == nameBeforeNumbers + "1457" || wallSpriteName == nameBeforeNumbers + "12457"){
            return nameBeforeNumbers + "157"; // west T junction
        }
        //north wall
        if(wallSpriteName == nameBeforeNumbers + "034567" || wallSpriteName == nameBeforeNumbers + "234567" || wallSpriteName == nameBeforeNumbers + "0234567"){
            return nameBeforeNumbers + "34567"; // north wall
        }
        //east wall
        if(wallSpriteName == nameBeforeNumbers + "012567" || wallSpriteName == nameBeforeNumbers + "014567" || wallSpriteName == nameBeforeNumbers + "0124567"){
            return nameBeforeNumbers + "01567"; // east wall
        }
        //south wall
        if(wallSpriteName == nameBeforeNumbers + "012347" || wallSpriteName == nameBeforeNumbers + "012367" || wallSpriteName == nameBeforeNumbers + "0123467"){
            return nameBeforeNumbers + "01237"; // south wall
        }
        //west wall
        if(wallSpriteName == nameBeforeNumbers + "012345" || wallSpriteName == nameBeforeNumbers + "123456" || wallSpriteName == nameBeforeNumbers + "0123456"){
            return nameBeforeNumbers + "12345"; // west wall
        }

        if(wallSpriteName == nameBeforeNumbers + "01257" || wallSpriteName == nameBeforeNumbers + "01457" || wallSpriteName == nameBeforeNumbers + "012457"){
            return nameBeforeNumbers + "0157"; // inner t junction NSW1
        }
        if(wallSpriteName == nameBeforeNumbers + "12567" || wallSpriteName == nameBeforeNumbers + "14567" || wallSpriteName == nameBeforeNumbers + "124567"){
            return nameBeforeNumbers + "1567"; // inner t junction NSW2
        }

        if(wallSpriteName == nameBeforeNumbers + "01235" || wallSpriteName == nameBeforeNumbers + "12356" || wallSpriteName == nameBeforeNumbers + "012356"){
            return nameBeforeNumbers + "1235"; // inner t junction NES1
        }

        if(wallSpriteName == nameBeforeNumbers + "01345" || wallSpriteName == nameBeforeNumbers + "13456" || wallSpriteName == nameBeforeNumbers + "013456"){
            return nameBeforeNumbers + "1345"; // inner t junction NES2
        }

        if(wallSpriteName == nameBeforeNumbers + "12347" || wallSpriteName == nameBeforeNumbers + "12367" || wallSpriteName == nameBeforeNumbers + "123467"){
            return nameBeforeNumbers + "1237"; // inner t junction NEW1
        }
        if(wallSpriteName == nameBeforeNumbers + "01347" || wallSpriteName == nameBeforeNumbers + "01367" || wallSpriteName == nameBeforeNumbers + "013467"){
            return nameBeforeNumbers + "0137"; // inner t junction NEW2
        }
        if(wallSpriteName == nameBeforeNumbers + "03457" || wallSpriteName == nameBeforeNumbers + "23457" || wallSpriteName == nameBeforeNumbers + "023457"){
            return nameBeforeNumbers + "3457"; // inner t junction SEW1
        }
        if(wallSpriteName == nameBeforeNumbers + "03567" || wallSpriteName == nameBeforeNumbers + "23567" || wallSpriteName == nameBeforeNumbers + "023567"){
            return nameBeforeNumbers + "3567"; // inner t junction SEW
        }
        return wallSpriteName;
    }

    public static TileDetail[] GetAdjacentTileDetails(Tile tile){
        TileDetail[] adjacentTileDetails = new TileDetail[8];
        for (int i = 0; i < 8; i++)
        {
            int x = 0;
            int y = 0;
            switch (i)
            {
                case 0:
                    x = -1; y = 1; break;
                case 1:
                    x = 0; y = 1; break;
                case 2:
                    x = 1; y = 1; break;
                case 3:
                    x = 1; y = 0; break;
                case 4:
                    x = 1; y = -1; break;
                case 5:
                    x = 0; y = -1; break;
                case 6:
                    x = -1; y = -1; break;
                case 7:
                    x = -1; y = 0; break;
                default:
                    x = 0; y = 0; break;
            }
            Vector2Int tilePosition = new Vector2Int(tile.GlobalPosX + x, tile.GlobalPosY + y);
            Tile adjacentTile = WorldController.Instance.GetTileFromGlobalPosition(tilePosition);
            if(adjacentTile != null){
                adjacentTileDetails[i] = adjacentTile.TileDetailData;
            }
        }
        return adjacentTileDetails;
    }
}