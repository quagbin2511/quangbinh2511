window.addEventListener('DOMContentLoaded', () =>
   {
    var btnSocial = document.getElementById('btn__social');
    var socialTimes = document.getElementById('social__times');
    var socialItem = document.querySelectorAll('.social__item');
    var isTrue = true;    
    btnSocial.addEventListener('click', rotateSocial);
    function rotateSocial() {
        if (isTrue) {
            socialTimes.style.color = "#f00";
            btnSocial.style.border = "1px solid #f00";  
            for (let i = 0; i < socialItem.length; i++) {
                socialItem[i].classList.add('active');
            }
            socialItem[0].style.transform = "translateY(-50px) rotate(720deg)"; 
            socialItem[1].style.transform = "translate(-50px, -25px) rotate(720deg)"; 
            socialItem[2].style.transform = "translate(-50px, 25px) rotate(720deg)"; 
            socialItem[3].style.transform = "translateY(50px) rotate(720deg)"; 
            isTrue = false;
        } else {
            socialTimes.style.color = "#30c130";
            btnSocial.style.border = "1px solid #30c130";
            for (let i = 0; i < socialItem.length; i++) {
                socialItem[i].style.transform = "translate(0)";
            }           
            isTrue = true;
        }
    }
}
);