import pandas as pd
import matplotlib.pyplot as plt
import pandas as pd

import statsmodels.api as sm
from scipy.stats import norm
from scipy import stats
import pylab

my_data = norm.rvs(size=1000)
sm.qqplot(my_data, line='45')
my_data = norm.rvs(size=500)
print(stats.shapiro(my_data))


print("abraka")
normal = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\normal.csv")
normal.columns = ["normal"]

normal_shifted = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\normal_mean_42_stdev_13.csv")
normal_shifted.columns = ["normal_shifted"]

exponential = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\exponential.csv")
exponential.columns = ["exponential"]

normal.hist(bins=100)
exponential.hist(bins=100)
normal_shifted.hist(bins=100)
plt.show()

sm.qqplot(normal, line='45')
sm.qqplot(exponential, line='45')
pylab.show()

# if pvalue > 0.05 we assume a normal distribution
print("normal shapiro")
print(stats.shapiro(normal))
print("dabra")
