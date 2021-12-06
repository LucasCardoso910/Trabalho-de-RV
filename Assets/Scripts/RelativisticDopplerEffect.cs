using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Fazendo o foguete rodar ok
// Conseguir o angulo de rotação do foguete ok
// Conseguier o ângulo do efeito doppler do foguete ok
// Aplicar o efeito doppler ao foguete
// Criar forma de controlar a velocidade

public class RelativisticDopplerEffect : MonoBehaviour
{
    public GameObject rotor_center;
    public GameObject spaceShip;
    public GameObject player;

    private Dictionary<int, string> wavelength_rgb = new Dictionary<int, string>();
    private RotatorRocket rotator;
    private double c = 100; //300000000
    private const int initial_wavelength = 590;
    private float radius;
    private float distToCenter;
    
    void Start()
    {
        rotator = rotor_center.GetComponent<RotatorRocket>();
        radius = getRadius();
        distToCenter = getDistPlayerCenter();
        start_dict();
        //velocity = spaceShip.GetComponent<spaceShipVelocity>();
        //read_file();
    }

    void Update()
    {
        // Debug.Log(get_angle(rotor_center));
        double theta = get_or(centralAngle(rotor_center));
        // Debug.Log(theta);
        updateColor((int) velocity_to_wavelength(initial_wavelength, rotator.speed, theta));
    }

    float getRadius()
    {
        Vector3 rotor_position = rotor_center.transform.position;
        Vector3 rocket_position = spaceShip.transform.position;

        return Vector3.Distance(rotor_position, rocket_position);
    }

    float getDistPlayerCenter()
    {
        Vector3 rotor_position = rotor_center.transform.position;
        Vector3 player_position = player.transform.position;

        return Vector3.Distance(rotor_position, player_position);
    }
    
    void read_file()
    {
        string[] text = System.IO.File.ReadAllLines(@"Etc/cores.txt");
        foreach (string line in text)
        {
            string[] substrings = line.Split(' ');
            int wavelength = int.Parse(substrings[0]);
            string rgb = substrings[1];

            wavelength_rgb.Add(wavelength, rgb);

            //Debug.Log(line);
        }
    }

    double velocity_to_wavelength(double initial_wavelength, double velocity, double theta)
    {
        // fr = fs / (y * (1 + beta * cos(theta)))
        // v = lambda * f -> f = v / lambda
        // wr = ws * (y * (1 + beta * cos(theta)))
        double beta = velocity / c;
        double gama = 1 / (Math.Sqrt(1 - Math.Pow(beta,2)));
        return initial_wavelength * (gama * (1 + beta * Math.Cos(theta)));
    } 

    Vector3 get_rocket_position() {
        float x = rotor_center.transform.position.x;
        float y = rotor_center.transform.position.y;
        float z = rotor_center.transform.position.z;
        float alpha = degreesToRad(rotor_center.transform.eulerAngles.y);

        Vector3 position = new Vector3(x + (float) Math.Cos(alpha) * radius, y, z + (float) Math.Sin(alpha) * radius);
        //Debug.Log(position);
        return position;
    }

    double get_or(double alpha){

        double beta = Math.Atan(Math.Sin(alpha) * radius /(distToCenter-radius*Math.Cos(alpha)));
        
        double Or = (-Math.PI / 2) + alpha + beta;

        return Or;
    }

    float centralAngle(GameObject rotor_center){
        float rotation = rotor_center.transform.eulerAngles.y;
        return degreesToRad(rotation);
    }

    double get_angle(GameObject rotor_center) {
        // Vector1: From observer to rocket
        // Vector2: From rotor_center to rocket

        Vector3 rocket_position = get_rocket_position();
        Vector3 v1 = player.transform.position - rocket_position;
        Vector3 v2 = rocket_position - rotor_center.transform.position;
        return Vector3.Angle(v1, v2);

        // float rotation = rotor_center.transform.eulerAngles.y;
        // // return (Math.PI/2 - degreesToRad(rotation));
        // return (degreesToRad(rotation) + Math.PI/2);
    }

    void updateColor(int wavelength) {
        Debug.Log(wavelength);
        var renderer = spaceShip.GetComponent<Renderer>();

        int red, green, blue;
        Color newColor;

        try {
            string hex = wavelength_rgb[wavelength];
            string red_string, green_string, blue_string;

            red_string = hex.Substring(0, 2);
            green_string = hex.Substring(2, 2);
            blue_string = hex.Substring(4, 2);
            
            red = Int32.Parse(red_string, System.Globalization.NumberStyles.HexNumber);
            green = Int32.Parse(green_string, System.Globalization.NumberStyles.HexNumber);
            blue = Int32.Parse(blue_string, System.Globalization.NumberStyles.HexNumber);
        }
        catch (Exception e)
        {
            red = 0;
            green = 0;
            blue = 0;
        }
        
        newColor = new Color(red/255f, green/255f, blue/255f);
        renderer.material.SetColor("_Color", newColor);
    }

    private float radToDegrees(float radAngle)
    {
        return (radAngle * 360) / (float) (2 * Math.PI);
    }

    private float degreesToRad(float degAngle)
    {
        return (degAngle * (float) (2 * Math.PI)) / 360;
    }
    
    void start_dict() {
        wavelength_rgb.Add(380, "610061");
        wavelength_rgb.Add(381, "640066");
        wavelength_rgb.Add(382, "67006a");
        wavelength_rgb.Add(383, "6a006f");
        wavelength_rgb.Add(384, "6d0073");
        wavelength_rgb.Add(385, "6f0077");
        wavelength_rgb.Add(386, "72007c");
        wavelength_rgb.Add(387, "740080");
        wavelength_rgb.Add(388, "760084");
        wavelength_rgb.Add(389, "780088");
        wavelength_rgb.Add(390, "79008d");
        wavelength_rgb.Add(391, "7b0091");
        wavelength_rgb.Add(392, "7c0095");
        wavelength_rgb.Add(393, "7e0099");
        wavelength_rgb.Add(394, "7f009d");
        wavelength_rgb.Add(395, "8000a1");
        wavelength_rgb.Add(396, "8100a5");
        wavelength_rgb.Add(397, "8100a9");
        wavelength_rgb.Add(398, "8200ad");
        wavelength_rgb.Add(399, "8200b1");
        wavelength_rgb.Add(400, "8300b5");
        wavelength_rgb.Add(401, "8300b9");
        wavelength_rgb.Add(402, "8300bc");
        wavelength_rgb.Add(403, "8300c0");
        wavelength_rgb.Add(404, "8200c4");
        wavelength_rgb.Add(405, "8200c8");
        wavelength_rgb.Add(406, "8100cc");
        wavelength_rgb.Add(407, "8100cf");
        wavelength_rgb.Add(408, "8000d3");
        wavelength_rgb.Add(409, "7f00d7");
        wavelength_rgb.Add(410, "7e00db");
        wavelength_rgb.Add(411, "7c00de");
        wavelength_rgb.Add(412, "7b00e2");
        wavelength_rgb.Add(413, "7900e6");
        wavelength_rgb.Add(414, "7800e9");
        wavelength_rgb.Add(415, "7600ed");
        wavelength_rgb.Add(416, "7400f1");
        wavelength_rgb.Add(417, "7100f4");
        wavelength_rgb.Add(418, "6f00f8");
        wavelength_rgb.Add(419, "6d00fb");
        wavelength_rgb.Add(420, "6a00ff");
        wavelength_rgb.Add(421, "6600ff");
        wavelength_rgb.Add(422, "6100ff");
        wavelength_rgb.Add(423, "5d00ff");
        wavelength_rgb.Add(424, "5900ff");
        wavelength_rgb.Add(425, "5400ff");
        wavelength_rgb.Add(426, "5000ff");
        wavelength_rgb.Add(427, "4b00ff");
        wavelength_rgb.Add(428, "4600ff");
        wavelength_rgb.Add(429, "4200ff");
        wavelength_rgb.Add(430, "3d00ff");
        wavelength_rgb.Add(431, "3800ff");
        wavelength_rgb.Add(432, "3300ff");
        wavelength_rgb.Add(433, "2e00ff");
        wavelength_rgb.Add(434, "2800ff");
        wavelength_rgb.Add(435, "2300ff");
        wavelength_rgb.Add(436, "1d00ff");
        wavelength_rgb.Add(437, "1700ff");
        wavelength_rgb.Add(438, "1100ff");
        wavelength_rgb.Add(439, "0a00ff");
        wavelength_rgb.Add(440, "0000ff");
        wavelength_rgb.Add(441, "000bff");
        wavelength_rgb.Add(442, "0013ff");
        wavelength_rgb.Add(443, "001bff");
        wavelength_rgb.Add(444, "0022ff");
        wavelength_rgb.Add(445, "0028ff");
        wavelength_rgb.Add(446, "002fff");
        wavelength_rgb.Add(447, "0035ff");
        wavelength_rgb.Add(448, "003bff");
        wavelength_rgb.Add(449, "0041ff");
        wavelength_rgb.Add(450, "0046ff");
        wavelength_rgb.Add(451, "004cff");
        wavelength_rgb.Add(452, "0051ff");
        wavelength_rgb.Add(453, "0057ff");
        wavelength_rgb.Add(454, "005cff");
        wavelength_rgb.Add(455, "0061ff");
        wavelength_rgb.Add(456, "0066ff");
        wavelength_rgb.Add(457, "006cff");
        wavelength_rgb.Add(458, "0071ff");
        wavelength_rgb.Add(459, "0076ff");
        wavelength_rgb.Add(460, "007bff");
        wavelength_rgb.Add(461, "007fff");
        wavelength_rgb.Add(462, "0084ff");
        wavelength_rgb.Add(463, "0089ff");
        wavelength_rgb.Add(464, "008eff");
        wavelength_rgb.Add(465, "0092ff");
        wavelength_rgb.Add(466, "0097ff");
        wavelength_rgb.Add(467, "009cff");
        wavelength_rgb.Add(468, "00a0ff");
        wavelength_rgb.Add(469, "00a5ff");
        wavelength_rgb.Add(470, "00a9ff");
        wavelength_rgb.Add(471, "00aeff");
        wavelength_rgb.Add(472, "00b2ff");
        wavelength_rgb.Add(473, "00b7ff");
        wavelength_rgb.Add(474, "00bbff");
        wavelength_rgb.Add(475, "00c0ff");
        wavelength_rgb.Add(476, "00c4ff");
        wavelength_rgb.Add(477, "00c8ff");
        wavelength_rgb.Add(478, "00cdff");
        wavelength_rgb.Add(479, "00d1ff");
        wavelength_rgb.Add(480, "00d5ff");
        wavelength_rgb.Add(481, "00daff");
        wavelength_rgb.Add(482, "00deff");
        wavelength_rgb.Add(483, "00e2ff");
        wavelength_rgb.Add(484, "00e6ff");
        wavelength_rgb.Add(485, "00eaff");
        wavelength_rgb.Add(486, "00efff");
        wavelength_rgb.Add(487, "00f3ff");
        wavelength_rgb.Add(488, "00f7ff");
        wavelength_rgb.Add(489, "00fbff");
        wavelength_rgb.Add(490, "00ffff");
        wavelength_rgb.Add(491, "00fff5");
        wavelength_rgb.Add(492, "00ffea");
        wavelength_rgb.Add(493, "00ffe0");
        wavelength_rgb.Add(494, "00ffd5");
        wavelength_rgb.Add(495, "00ffcb");
        wavelength_rgb.Add(496, "00ffc0");
        wavelength_rgb.Add(497, "00ffb5");
        wavelength_rgb.Add(498, "00ffa9");
        wavelength_rgb.Add(499, "00ff9e");
        wavelength_rgb.Add(500, "00ff92");
        wavelength_rgb.Add(501, "00ff87");
        wavelength_rgb.Add(502, "00ff7b");
        wavelength_rgb.Add(503, "00ff6e");
        wavelength_rgb.Add(504, "00ff61");
        wavelength_rgb.Add(505, "00ff54");
        wavelength_rgb.Add(506, "00ff46");
        wavelength_rgb.Add(507, "00ff38");
        wavelength_rgb.Add(508, "00ff28");
        wavelength_rgb.Add(509, "00ff17");
        wavelength_rgb.Add(510, "00ff00");
        wavelength_rgb.Add(511, "09ff00");
        wavelength_rgb.Add(512, "0fff00");
        wavelength_rgb.Add(513, "15ff00");
        wavelength_rgb.Add(514, "1aff00");
        wavelength_rgb.Add(515, "1fff00");
        wavelength_rgb.Add(516, "24ff00");
        wavelength_rgb.Add(517, "28ff00");
        wavelength_rgb.Add(518, "2dff00");
        wavelength_rgb.Add(519, "31ff00");
        wavelength_rgb.Add(520, "36ff00");
        wavelength_rgb.Add(521, "3aff00");
        wavelength_rgb.Add(522, "3eff00");
        wavelength_rgb.Add(523, "42ff00");
        wavelength_rgb.Add(524, "46ff00");
        wavelength_rgb.Add(525, "4aff00");
        wavelength_rgb.Add(526, "4eff00");
        wavelength_rgb.Add(527, "52ff00");
        wavelength_rgb.Add(528, "56ff00");
        wavelength_rgb.Add(529, "5aff00");
        wavelength_rgb.Add(530, "5eff00");
        wavelength_rgb.Add(531, "61ff00");
        wavelength_rgb.Add(532, "65ff00");
        wavelength_rgb.Add(533, "69ff00");
        wavelength_rgb.Add(534, "6cff00");
        wavelength_rgb.Add(535, "70ff00");
        wavelength_rgb.Add(536, "73ff00");
        wavelength_rgb.Add(537, "77ff00");
        wavelength_rgb.Add(538, "7bff00");
        wavelength_rgb.Add(539, "7eff00");
        wavelength_rgb.Add(540, "81ff00");
        wavelength_rgb.Add(541, "85ff00");
        wavelength_rgb.Add(542, "88ff00");
        wavelength_rgb.Add(543, "8cff00");
        wavelength_rgb.Add(544, "8fff00");
        wavelength_rgb.Add(545, "92ff00");
        wavelength_rgb.Add(546, "96ff00");
        wavelength_rgb.Add(547, "99ff00");
        wavelength_rgb.Add(548, "9cff00");
        wavelength_rgb.Add(549, "a0ff00");
        wavelength_rgb.Add(550, "a3ff00");
        wavelength_rgb.Add(551, "a6ff00");
        wavelength_rgb.Add(552, "a9ff00");
        wavelength_rgb.Add(553, "adff00");
        wavelength_rgb.Add(554, "b0ff00");
        wavelength_rgb.Add(555, "b3ff00");
        wavelength_rgb.Add(556, "b6ff00");
        wavelength_rgb.Add(557, "b9ff00");
        wavelength_rgb.Add(558, "bdff00");
        wavelength_rgb.Add(559, "c0ff00");
        wavelength_rgb.Add(560, "c3ff00");
        wavelength_rgb.Add(561, "c6ff00");
        wavelength_rgb.Add(562, "c9ff00");
        wavelength_rgb.Add(563, "ccff00");
        wavelength_rgb.Add(564, "cfff00");
        wavelength_rgb.Add(565, "d2ff00");
        wavelength_rgb.Add(566, "d5ff00");
        wavelength_rgb.Add(567, "d8ff00");
        wavelength_rgb.Add(568, "dbff00");
        wavelength_rgb.Add(569, "deff00");
        wavelength_rgb.Add(570, "e1ff00");
        wavelength_rgb.Add(571, "e4ff00");
        wavelength_rgb.Add(572, "e7ff00");
        wavelength_rgb.Add(573, "eaff00");
        wavelength_rgb.Add(574, "edff00");
        wavelength_rgb.Add(575, "f0ff00");
        wavelength_rgb.Add(576, "f3ff00");
        wavelength_rgb.Add(577, "f6ff00");
        wavelength_rgb.Add(578, "f9ff00");
        wavelength_rgb.Add(579, "fcff00");
        wavelength_rgb.Add(580, "ffff00");
        wavelength_rgb.Add(581, "fffc00");
        wavelength_rgb.Add(582, "fff900");
        wavelength_rgb.Add(583, "fff600");
        wavelength_rgb.Add(584, "fff200");
        wavelength_rgb.Add(585, "ffef00");
        wavelength_rgb.Add(586, "ffec00");
        wavelength_rgb.Add(587, "ffe900");
        wavelength_rgb.Add(588, "ffe600");
        wavelength_rgb.Add(589, "ffe200");
        wavelength_rgb.Add(590, "ffdf00");
        wavelength_rgb.Add(591, "ffdc00");
        wavelength_rgb.Add(592, "ffd900");
        wavelength_rgb.Add(593, "ffd500");
        wavelength_rgb.Add(594, "ffd200");
        wavelength_rgb.Add(595, "ffcf00");
        wavelength_rgb.Add(596, "ffcb00");
        wavelength_rgb.Add(597, "ffc800");
        wavelength_rgb.Add(598, "ffc500");
        wavelength_rgb.Add(599, "ffc100");
        wavelength_rgb.Add(600, "ffbe00");
        wavelength_rgb.Add(601, "ffbb00");
        wavelength_rgb.Add(602, "ffb700");
        wavelength_rgb.Add(603, "ffb400");
        wavelength_rgb.Add(604, "ffb000");
        wavelength_rgb.Add(605, "ffad00");
        wavelength_rgb.Add(606, "ffa900");
        wavelength_rgb.Add(607, "ffa600");
        wavelength_rgb.Add(608, "ffa200");
        wavelength_rgb.Add(609, "ff9f00");
        wavelength_rgb.Add(610, "ff9b00");
        wavelength_rgb.Add(611, "ff9800");
        wavelength_rgb.Add(612, "ff9400");
        wavelength_rgb.Add(613, "ff9100");
        wavelength_rgb.Add(614, "ff8d00");
        wavelength_rgb.Add(615, "ff8900");
        wavelength_rgb.Add(616, "ff8600");
        wavelength_rgb.Add(617, "ff8200");
        wavelength_rgb.Add(618, "ff7e00");
        wavelength_rgb.Add(619, "ff7b00");
        wavelength_rgb.Add(620, "ff7700");
        wavelength_rgb.Add(621, "ff7300");
        wavelength_rgb.Add(622, "ff6f00");
        wavelength_rgb.Add(623, "ff6b00");
        wavelength_rgb.Add(624, "ff6700");
        wavelength_rgb.Add(625, "ff6300");
        wavelength_rgb.Add(626, "ff5f00");
        wavelength_rgb.Add(627, "ff5b00");
        wavelength_rgb.Add(628, "ff5700");
        wavelength_rgb.Add(629, "ff5300");
        wavelength_rgb.Add(630, "ff4f00");
        wavelength_rgb.Add(631, "ff4b00");
        wavelength_rgb.Add(632, "ff4600");
        wavelength_rgb.Add(633, "ff4200");
        wavelength_rgb.Add(634, "ff3e00");
        wavelength_rgb.Add(635, "ff3900");
        wavelength_rgb.Add(636, "ff3400");
        wavelength_rgb.Add(637, "ff3000");
        wavelength_rgb.Add(638, "ff2b00");
        wavelength_rgb.Add(639, "ff2600");
        wavelength_rgb.Add(640, "ff2100");
        wavelength_rgb.Add(641, "ff1b00");
        wavelength_rgb.Add(642, "ff1600");
        wavelength_rgb.Add(643, "ff1000");
        wavelength_rgb.Add(644, "ff0900");
        wavelength_rgb.Add(645, "ff0000");
        wavelength_rgb.Add(646, "ff0000");
        wavelength_rgb.Add(647, "ff0000");
        wavelength_rgb.Add(648, "ff0000");
        wavelength_rgb.Add(649, "ff0000");
        wavelength_rgb.Add(650, "ff0000");
        wavelength_rgb.Add(651, "ff0000");
        wavelength_rgb.Add(652, "ff0000");
        wavelength_rgb.Add(653, "ff0000");
        wavelength_rgb.Add(654, "ff0000");
        wavelength_rgb.Add(655, "ff0000");
        wavelength_rgb.Add(656, "ff0000");
        wavelength_rgb.Add(657, "ff0000");
        wavelength_rgb.Add(658, "ff0000");
        wavelength_rgb.Add(659, "ff0000");
        wavelength_rgb.Add(660, "ff0000");
        wavelength_rgb.Add(661, "ff0000");
        wavelength_rgb.Add(662, "ff0000");
        wavelength_rgb.Add(663, "ff0000");
        wavelength_rgb.Add(664, "ff0000");
        wavelength_rgb.Add(665, "ff0000");
        wavelength_rgb.Add(666, "ff0000");
        wavelength_rgb.Add(667, "ff0000");
        wavelength_rgb.Add(668, "ff0000");
        wavelength_rgb.Add(669, "ff0000");
        wavelength_rgb.Add(670, "ff0000");
        wavelength_rgb.Add(671, "ff0000");
        wavelength_rgb.Add(672, "ff0000");
        wavelength_rgb.Add(673, "ff0000");
        wavelength_rgb.Add(674, "ff0000");
        wavelength_rgb.Add(675, "ff0000");
        wavelength_rgb.Add(676, "ff0000");
        wavelength_rgb.Add(677, "ff0000");
        wavelength_rgb.Add(678, "ff0000");
        wavelength_rgb.Add(679, "ff0000");
        wavelength_rgb.Add(680, "ff0000");
        wavelength_rgb.Add(681, "ff0000");
        wavelength_rgb.Add(682, "ff0000");
        wavelength_rgb.Add(683, "ff0000");
        wavelength_rgb.Add(684, "ff0000");
        wavelength_rgb.Add(685, "ff0000");
        wavelength_rgb.Add(686, "ff0000");
        wavelength_rgb.Add(687, "ff0000");
        wavelength_rgb.Add(688, "ff0000");
        wavelength_rgb.Add(689, "ff0000");
        wavelength_rgb.Add(690, "ff0000");
        wavelength_rgb.Add(691, "ff0000");
        wavelength_rgb.Add(692, "ff0000");
        wavelength_rgb.Add(693, "ff0000");
        wavelength_rgb.Add(694, "ff0000");
        wavelength_rgb.Add(695, "ff0000");
        wavelength_rgb.Add(696, "ff0000");
        wavelength_rgb.Add(697, "ff0000");
        wavelength_rgb.Add(698, "ff0000");
        wavelength_rgb.Add(699, "ff0000");
        wavelength_rgb.Add(700, "ff0000");
        wavelength_rgb.Add(701, "fd0000");
        wavelength_rgb.Add(702, "fb0000");
        wavelength_rgb.Add(703, "fa0000");
        wavelength_rgb.Add(704, "f80000");
        wavelength_rgb.Add(705, "f60000");
        wavelength_rgb.Add(706, "f40000");
        wavelength_rgb.Add(707, "f20000");
        wavelength_rgb.Add(708, "f10000");
        wavelength_rgb.Add(709, "ef0000");
        wavelength_rgb.Add(710, "ed0000");
        wavelength_rgb.Add(711, "eb0000");
        wavelength_rgb.Add(712, "e90000");
        wavelength_rgb.Add(713, "e80000");
        wavelength_rgb.Add(714, "e60000");
        wavelength_rgb.Add(715, "e40000");
        wavelength_rgb.Add(716, "e20000");
        wavelength_rgb.Add(717, "e00000");
        wavelength_rgb.Add(718, "de0000");
        wavelength_rgb.Add(719, "dc0000");
        wavelength_rgb.Add(720, "db0000");
        wavelength_rgb.Add(721, "d90000");
        wavelength_rgb.Add(722, "d70000");
        wavelength_rgb.Add(723, "d50000");
        wavelength_rgb.Add(724, "d30000");
        wavelength_rgb.Add(725, "d10000");
        wavelength_rgb.Add(726, "cf0000");
        wavelength_rgb.Add(727, "ce0000");
        wavelength_rgb.Add(728, "cc0000");
        wavelength_rgb.Add(729, "ca0000");
        wavelength_rgb.Add(730, "c80000");
        wavelength_rgb.Add(731, "c60000");
        wavelength_rgb.Add(732, "c40000");
        wavelength_rgb.Add(733, "c20000");
        wavelength_rgb.Add(734, "c00000");
        wavelength_rgb.Add(735, "be0000");
        wavelength_rgb.Add(736, "bc0000");
        wavelength_rgb.Add(737, "ba0000");
        wavelength_rgb.Add(738, "b90000");
        wavelength_rgb.Add(739, "b70000");
        wavelength_rgb.Add(740, "b50000");
        wavelength_rgb.Add(741, "b30000");
        wavelength_rgb.Add(742, "b10000");
        wavelength_rgb.Add(743, "af0000");
        wavelength_rgb.Add(744, "ad0000");
        wavelength_rgb.Add(745, "ab0000");
        wavelength_rgb.Add(746, "a90000");
        wavelength_rgb.Add(747, "a70000");
        wavelength_rgb.Add(748, "a50000");
        wavelength_rgb.Add(749, "a30000");
        wavelength_rgb.Add(750, "a10000");
        wavelength_rgb.Add(751, "9f0000");
        wavelength_rgb.Add(752, "9d0000");
        wavelength_rgb.Add(753, "9b0000");
        wavelength_rgb.Add(754, "990000");
        wavelength_rgb.Add(755, "970000");
        wavelength_rgb.Add(756, "950000");
        wavelength_rgb.Add(757, "930000");
        wavelength_rgb.Add(758, "910000");
        wavelength_rgb.Add(759, "8f0000");
        wavelength_rgb.Add(760, "8d0000");
        wavelength_rgb.Add(761, "8a0000");
        wavelength_rgb.Add(762, "880000");
        wavelength_rgb.Add(763, "860000");
        wavelength_rgb.Add(764, "840000");
        wavelength_rgb.Add(765, "820000");
        wavelength_rgb.Add(766, "800000");
        wavelength_rgb.Add(767, "7e0000");
        wavelength_rgb.Add(768, "7c0000");
        wavelength_rgb.Add(769, "7a0000");
        wavelength_rgb.Add(770, "770000");
        wavelength_rgb.Add(771, "750000");
        wavelength_rgb.Add(772, "730000");
        wavelength_rgb.Add(773, "710000");
        wavelength_rgb.Add(774, "6f0000");
        wavelength_rgb.Add(775, "6d0000");
        wavelength_rgb.Add(776, "6a0000");
        wavelength_rgb.Add(777, "680000");
        wavelength_rgb.Add(778, "660000");
        wavelength_rgb.Add(779, "640000");
        wavelength_rgb.Add(780, "610000");
    }
}