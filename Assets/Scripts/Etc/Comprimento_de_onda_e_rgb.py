# URl: https://www.johndcook.com/wavelength_to_RGB.html

import pathlib
from os import listdir
from selenium import webdriver
from selenium.webdriver.common.by import By
from selenium.webdriver.chrome.options import Options
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC

tabela = []

browser = webdriver.Chrome(executable_path='/home/paulo/webdrivers/chromedriver')
browser.get('https://www.johndcook.com/wavelength_to_RGB.html')


for i in range(380,781):
    inputBox = browser.find_element_by_id('in')
    inputBox.send_keys(i)
    browser.find_element_by_name('B1').click()
    inputBox.clear()

    output = browser.find_element_by_id('result')
    rgb = output.text.split('#')[-1]
    
    tabela.append(rgb)


with open("Resultado.txt","a") as f:
    for i in range(0,401):
        f.write(str(i + 380) + ' ' + str(tabela[i]) + '\n')