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
normal = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\normal.csv")
normal.columns = ["normal"]

normal_shifted = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\normal_mean_42_stdev_13.csv")
normal_shifted.columns = ["normal_shifted"]

exponential = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential.csv")
exponential.columns = ["exponential"]

exponential5 = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential5.csv")
exponential5.columns = ["exponential5"]

exponential_minus10 = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\exponential_-10.csv")
exponential_minus10.columns = ["exponential -10"]

exponential_shifting_not_shifted = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\ExponentialShiftingNotShifted.csv")
exponential_shifting_not_shifted.columns = ["exp shifting 1"]

geometric = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Geometric0.5.csv")
geometric.columns = ["Geometric"]

binomial = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\Binomial.csv")
binomial.columns = ["Binomial"]

binomial_naive = pd.read_csv(filepath_or_buffer="D:\\MFF\\LS_2020\\cSharp\\Faker\\bakalarka\\ValidateDistributions\\BinomialNaive.csv")
binomial_naive.columns = ["Binomial naive"]

normal.hist(bins=100)
exponential.hist(bins=100)
normal_shifted.hist(bins=100)
exponential5.hist(bins=100)
exponential_minus10.hist(bins=100)
exponential_shifting_not_shifted.hist(bins=100)
geometric.hist(bins=100)
binomial.hist(bins=100)
binomial_naive.hist(bins=100)
plt.show()

sm.qqplot(normal, line='45')
sm.qqplot(exponential, line='45')
#pylab.show()

# if pvalue > 0.05 we assume a normal distribution
print("normal shapiro")
print(stats.shapiro(normal))
print("dabra")
