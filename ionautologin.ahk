/*
This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
	
	Main Author: T.V.VIGNESH - tvvignesh@techahoy.in - http://www.facebook.com/tvvignesh
	Further versions will be updated soon. 
*/

ID:=""
PASS:=""
Loop, %0%  ; For each parameter:
{
    param := %A_Index%  ; Fetch the contents of the variable whose name is contained in A_Index.
    if(A_Index=1){
	ID:=param
	}
	if(A_Index=2){
	PASS:=param
	}
    
}
Run http://172.16.16.16/24online/webpages/client.jsp
sleep 4000
send %ID%{TAB}%PASS%{enter}
sleep 3000
Send ^w
